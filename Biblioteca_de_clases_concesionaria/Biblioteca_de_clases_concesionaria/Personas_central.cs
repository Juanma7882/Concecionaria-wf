using Biblioteca_de_clases_concesionaria.Carpeta_personas;
using Biblioteca_de_clases_concesionaria.Cvehiculos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca_de_clases_concesionaria
{
    public class Personas_central 
    {
        private List<Persona> listaDeLLamadas;
        private readonly string currentDirectory;

        private Daojson manipuladorAdministrador;
        private Daojson manipuladorEmpleado;
        private Daojson manipuladorCliente;

        public Personas_central()
        {
            this.listaDeLLamadas = new List<Persona>();

            string projectDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string relativePath = Path.Combine(projectDirectory, @"..\..\..\Aplicasion_WF_Consecionaria\archivosjs\");
            currentDirectory = Path.GetFullPath(relativePath);

            InicializarClases();
        }

        public List<Persona> Llamadas
        {
            get { return this.listaDeLLamadas; }
        }

        private void AgregarLlamada(Persona nuevaLlamada)
        {
            this.listaDeLLamadas.Add(nuevaLlamada);
            guardartipodellamada();
        }

        private void InicializarClases()
        {
            this.manipuladorAdministrador = new Daojson(currentDirectory, "Administrador.json");
            this.manipuladorEmpleado = new Daojson(currentDirectory, "Empleado.json");
            this.manipuladorCliente = new Daojson(currentDirectory, "Cliente.json");
        }

        private void guardartipodellamada()
        {
            foreach (var llamada in listaDeLLamadas)
            {
                if (llamada is Administrador administrador)
                {
                    this.manipuladorAdministrador.Crear();
                    List<Administrador> nuevoadministrador = new List<Administrador> { administrador };
                    this.manipuladorAdministrador.AgregarElementosJson(nuevoadministrador);
                }
                if (llamada is Empleado Empleado)
                {
                    this.manipuladorEmpleado.Crear();
                    List<Empleado> nuevoEmpleado = new List<Empleado> { Empleado };
                    this.manipuladorEmpleado.AgregarElementosJson(nuevoEmpleado);
                }

                if (llamada is Cliente cliente)
                {
                    this.manipuladorCliente.Crear();
                    List<Cliente> nuevocliente = new List<Cliente> { cliente };
                    this.manipuladorCliente.AgregarElementosJson(nuevocliente);
                }
               
            }
        }


        private void eliminartipollamada(int eliminar)
        {
            Task.Run(() =>
            {
            this.manipuladorAdministrador.BorrarUnaClase<Administrador>("Dni", eliminar);
            }
            );
            Task.Run(() =>
            {
            this.manipuladorEmpleado.BorrarUnaClase<Empleado>("Dni", eliminar);
            }
            );
            Task.Run(() =>
            {
            this.manipuladorCliente.BorrarUnaClase<Cliente>("Dni", eliminar);
            }
            ); 
        }


        public List<Administrador> LeerAdministrador()
        {
            List<Administrador> listaAdministradores = this.manipuladorAdministrador.Leer<Administrador>();
            return listaAdministradores;
        }

        public List<Empleado> LeerEmpleado()
        {
            List<Empleado> listaEmpleados = this.manipuladorEmpleado.Leer<Empleado>();
            return listaEmpleados;
        }


        public List<Cliente> LeerCliente()
        {
            List<Cliente> listaClientes = this.manipuladorCliente.Leer<Cliente>();
            return listaClientes;
        }


        public static Personas_central operator -(Personas_central c, int nuevaLlamada)
        {
            c.eliminartipollamada(nuevaLlamada);
            return c;
        }


        public static Personas_central operator +(Personas_central c, Persona nuevaLlamada)
        {
            if (nuevaLlamada != null)
            {
                c.AgregarLlamada(nuevaLlamada);
            }
            return c;
        }


        private bool ContienePersona(Persona persona)
        {
            if (persona == null)
            {
                return false;
            }

            if (persona is Administrador administrador)
            {
                List<Administrador> listaExistente = this.manipuladorAdministrador.Leer<Administrador>();
                return listaExistente.Any(v => v.Dni == administrador.Dni);
            }
            else if (persona is Empleado empleado)
            {
                List<Empleado> listaExistente = this.manipuladorEmpleado.Leer<Empleado>();
                return listaExistente.Any(v => v.Dni == empleado.Dni);
            }
            else if (persona is Cliente cliente)
            {
                List<Cliente> listaExistente = this.manipuladorCliente.Leer<Cliente>();
                return listaExistente.Any(v => v.Dni == cliente.Dni);
            }
            return false;
        }

        public List<T> Ordenar_por_dato<T>(string dato, List<T> lista, bool ascendente = true)
        {
            PropertyInfo propiedad = typeof(T).GetProperty(dato, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (propiedad == null)
            {
                throw new ArgumentException($"El dato '{dato}' proporcionado no es válido para ordenar.");
            }

            if (ascendente)
            {
                return lista.OrderBy(v => propiedad.GetValue(v, null)).ToList();
            }
            else
            {
                return lista.OrderByDescending(v => propiedad.GetValue(v, null)).ToList();
            }
        }

        public static bool operator ==(Personas_central c, Persona nuevaLlamada)
        {
            return c.ContienePersona(nuevaLlamada);
        }

        public static bool operator !=(Personas_central c, Persona nuevaLlamada)
        {
            return !c.ContienePersona(nuevaLlamada);
        }


        public override bool Equals(object obj)
        {
            return Equals(obj as Personas_central);
        }

        public bool Equals(Personas_central other)
        {
            return !(other is null) &&
                    EqualityComparer<List<Persona>>.Default.Equals(listaDeLLamadas, other.listaDeLLamadas) &&
                    currentDirectory == other.currentDirectory &&
                    EqualityComparer<List<Persona>>.Default.Equals(Llamadas, other.Llamadas);
        }

        public override int GetHashCode()
        {
            int hashCode = -273525045;
            hashCode = hashCode * -1521134295 + EqualityComparer<List<Persona>>.Default.GetHashCode(listaDeLLamadas);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(currentDirectory);
            hashCode = hashCode * -1521134295 + EqualityComparer<List<Persona>>.Default.GetHashCode(Llamadas);
            return hashCode;
        }
    }
}
