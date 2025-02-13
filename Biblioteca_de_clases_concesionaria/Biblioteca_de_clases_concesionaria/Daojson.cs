using Biblioteca_de_clases_concesionaria.carpeta_Excepcion;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
//using Newtonsoft.Json;



namespace Biblioteca_de_clases_concesionaria
{
    public class Daojson : ICrud
    {
        //campos de instancia
        private readonly string path;
        private readonly string Nombre;
        private readonly string RutaCompleta;

        public Daojson(string PathDelJson, string NombreJson)
        {
            this.path = PathDelJson;
            this.Nombre = NombreJson;
            this.RutaCompleta = Path.Combine(PathDelJson, NombreJson);

        }

        //metodos de clase
        public string Crear()
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                //string RutaCompleta = Path.Combine(path, Nombre);

                if (!File.Exists(this.RutaCompleta))
                {
                    File.WriteAllText(RutaCompleta, "[]"); // Escribir un array JSON vacío
                    return $"El archivo fue creado con éxito en la dirección {path} con el nombre {Nombre}\n";
                }
                else
                {
                    return "El archivo ya existe.\n";
                }
            }
            catch (Exception ex)
            {
                throw new ExcepcionJson($"Se produjo un ERROR  al crear el archivo JSON: {ex.Message}\n");
            }
        }

        public string Escribir<T>(List<T> lista)
        {
            try
            {
                if (!Directory.Exists(this.path))
                {
                    Directory.CreateDirectory(this.path);
                }
                string jsonString = JsonSerializer.Serialize(lista, new JsonSerializerOptions { WriteIndented = true });

                File.WriteAllText(this.RutaCompleta, jsonString);

                //return $"El archivo JSON fue escrito con éxito en la dirección {path} con el nombre {Nombre}\n";
                return $"El archivo JSON fue escrito con éxito";
            }
            catch (Exception ex)
            {
                throw new ExcepcionJson($"Se produjo un ERROR al escribir el archivo JSON: {ex.Message}\n");
            }
        }


        public List<T> Leer<T>()
        {
            try
            {
                if (!File.Exists(RutaCompleta))
                {
                    Crear();
                }
                using (StreamReader sr = new StreamReader(RutaCompleta))
                {
                    string jsonString = sr.ReadToEnd();
                    sr.Close();
                    if (string.IsNullOrWhiteSpace(jsonString) || jsonString == "[]")
                    {
                        // Si el JSON está vacío o solo contiene un array vacío, devolver una lista vacía
                        return new List<T>();
                    }
                    return JsonSerializer.Deserialize<List<T>>(jsonString);
                }
            }
            catch (Exception ex)
            {
                throw new ExcepcionJson($"Se produjo un error al leer el archivo JSON: {ex.Message}\n");
            }
        }

        public string AgregarElementosJson<T>(List<T> nuevosElementos)
        {
            try
            {
                List<T> listaExistente = Leer<T>();

                // Normalizar los nuevos elementos
                List<T> elementosNormalizados = nuevosElementos.Select(NormalizeObject).ToList();

                // Filtrar los nuevos elementos para evitar duplicados
                List<T> elementosAAgregar = elementosNormalizados.Where(nuevoElemento => !listaExistente.Contains(nuevoElemento)).ToList();

                if (elementosAAgregar.Count == 0)
                {
                    return "No se agregaron elementos porque ya existen en la lista.\n";
                }

                // Agregar los nuevos elementos a la lista existente
                listaExistente.AddRange(elementosAAgregar);

                // Escribir la lista actualizada al archivo JSON
                return Escribir(listaExistente);
            }
            catch (Exception ex)
            {
                throw new ExcepcionJson(ex.Message);
            }
        }

        public string Borrar<T>(string buscar, string nombreEmpleado)
        {
            try
            {
                List<T> listaExistente = Leer<T>();

                int indice = listaExistente.FindIndex(emp =>
                {
                    var propiedades = typeof(T).GetProperties();
                    foreach (var propiedad in propiedades)
                    {
                        if (propiedad.Name == buscar && propiedad.GetValue(emp).ToString() == nombreEmpleado)
                        {
                            return true;
                        }
                    }
                    return false;
                });
                if (indice >= 0)
                {
                    listaExistente.RemoveAt(indice);
                    return Escribir(listaExistente);
                }
                else
                {
                    return $"No se encontró un empleado con el nombre {nombreEmpleado}.\n";
                }
            }
            catch (Exception ex)
            {
                string mensaje = " error al eliminar el elemento del archivo JSON: \n";
                throw new ExcepcionJson(mensaje, ex.InnerException);
            }
        }

        public string BorrarUnaClase<T>(string buscar, int nombreEmpleado)
        {
            try
            {
                List<T> listaExistente = Leer<T>();

                int indice = listaExistente.FindIndex(emp =>
                {
                    var propiedades = typeof(T).GetProperties();
                    foreach (var propiedad in propiedades)
                    {
                        if (propiedad.Name == buscar && propiedad.GetValue(emp).ToString() == nombreEmpleado.ToString())
                        {
                            return true;
                        }
                    }
                    return false;
                });
                if (indice >= 0)
                {
                    listaExistente.RemoveAt(indice);
                    return Escribir(listaExistente);
                }
                else
                {
                    return $"No se encontró un empleado con el nombre {nombreEmpleado}.\n";
                }
            }
            catch (Exception ex)
            {
                string mensaje = " error al eliminar el elemento del archivo JSON: \n";
                throw new ExcepcionJson(mensaje, ex.InnerException);
            }
        }

        private string Capitalize(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            char primeraLetra = char.ToUpper(input[0]);
            string resto = input.Substring(1).ToLower();

            return primeraLetra + resto;
        }

        private T NormalizeObject<T>(T obj)
        {
            var propiedades = typeof(T).GetProperties();
            foreach (var propiedad in propiedades)
            {
                if (propiedad.PropertyType == typeof(string))
                {
                    string valorOriginal = (string)propiedad.GetValue(obj);
                    if (valorOriginal != null)
                    {
                        string valorCapitalizado = Capitalize(valorOriginal);
                        propiedad.SetValue(obj, valorCapitalizado);
                    }
                }
            }
            return obj;
        }
    }
}
