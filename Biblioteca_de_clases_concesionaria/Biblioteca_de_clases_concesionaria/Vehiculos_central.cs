using Biblioteca_de_clases_concesionaria.Cvehiculos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Biblioteca_de_clases_concesionaria.Cvehiculos.Vehiculo;

namespace Biblioteca_de_clases_concesionaria
{
    //delegate
    public delegate void PreInicicalizarCentralVehiculosHandler(object sender, EventArgs e);


    //public class Vehiculos_central : IEquatable<Vehiculos_central>
    public class Vehiculos_central 
    {
        private List<Vehiculo> listaDeLLamadas;
        private readonly string currentDirectory2;

        private Daojson manipuladorjsonAuto;
        private Daojson manipuladorjsonCamion;
        private Daojson manipuladorjsonDeportivo;
        private Daojson manipuladorjsonMoto;

        // event
        public event PreInicicalizarCentralVehiculosHandler InicializarVehiculos ;
            
        public Vehiculos_central()
        {
            this.listaDeLLamadas = new List<Vehiculo>();

            string projectDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string relativePath = Path.Combine(projectDirectory, @"..\..\..\Aplicasion_WF_Consecionaria\archivosjs\");
            currentDirectory2 = Path.GetFullPath(relativePath);

            InicializarClases();
        }
            
        public virtual void PreInicializadorVeihculosCentral()
        {
            // Invoca el evento si no es null
            InicializarVehiculos?.Invoke(this, EventArgs.Empty);
            Vehiculos_central vehiculos = new Vehiculos_central();
        }

        public List<Vehiculo> Llamadas
        {
            get { return this.listaDeLLamadas; }
        }

        private void AgregarLlamada(Vehiculo nuevaLlamada)
        {
            this.listaDeLLamadas.Add(nuevaLlamada);
            guardartipodellamada();
        }


        private void InicializarClases()
        {
            this.manipuladorjsonAuto = new Daojson(currentDirectory2, "Auto.json");
            this.manipuladorjsonCamion = new Daojson(currentDirectory2, "Camion.json");
            this.manipuladorjsonDeportivo = new Daojson(currentDirectory2, "Deportivo.json");
            this.manipuladorjsonMoto = new Daojson(currentDirectory2, "Moto.json");
        }


        private void guardartipodellamada()
        {
            foreach (var llamada in listaDeLLamadas)
            {
                if (llamada is Auto auto)
                {
                    this.manipuladorjsonAuto.Crear();
                    List<Auto> nuevoauto = new List<Auto> { auto };
                    this.manipuladorjsonAuto.AgregarElementosJson(nuevoauto);
                }
                if (llamada is Camion camion)
                {
                    this.manipuladorjsonCamion.Crear();
                    List<Camion> nuevocamion = new List<Camion> { camion };
                    this.manipuladorjsonCamion.AgregarElementosJson(nuevocamion);
                }

                if (llamada is Deportivo deportivo)
                {
                    this.manipuladorjsonDeportivo.Crear();
                    List<Deportivo> nuevodeportivo = new List<Deportivo> { deportivo };
                    this.manipuladorjsonDeportivo.AgregarElementosJson(nuevodeportivo);
                }
                if (llamada is Moto moto)
                {
                    this.manipuladorjsonMoto.Crear();
                    List<Moto> nuevomoto = new List<Moto> { moto };
                    this.manipuladorjsonMoto.AgregarElementosJson(nuevomoto);
                }
            }
        }


        private void eliminartipollamada(string eliminar)
        {
            Task.Run(() =>
            {
                this.manipuladorjsonAuto.Borrar<Auto>("matricula", eliminar);
            }
            );
            Task.Run(() =>
            {
                this.manipuladorjsonCamion.Borrar<Camion>("matricula", eliminar);
            }
            );
            Task.Run(() =>
            {
                this.manipuladorjsonDeportivo.Borrar<Deportivo>("matricula", eliminar);

            }
            ); Task.Run(() =>
            {
                this.manipuladorjsonMoto.Borrar<Moto>("matricula", eliminar);
            }
            );
        }


        public List<Auto> LeerAuto()
        {
            List<Auto> listaAutos = this.manipuladorjsonAuto.Leer<Auto>();
            return listaAutos;
        }


        public List<Moto> LeerMoto()
        {
            List<Moto> listaMotos = this.manipuladorjsonMoto.Leer<Moto>();
            return listaMotos;
        }


        public List<Deportivo> LeerDeportivo()
        {
            List<Deportivo> listaDeportivos = this.manipuladorjsonDeportivo.Leer<Deportivo>();
            return listaDeportivos;
        }


        public List<Camion> LeerCamion()
        {
            List<Camion> listaCamiones = this.manipuladorjsonCamion.Leer<Camion>();
            return listaCamiones;
        }



        public static Vehiculos_central operator -(Vehiculos_central c, string nuevaLlamada)
        {
            if (nuevaLlamada != null)
            {
                c.eliminartipollamada(nuevaLlamada);
            }
            return c;
        }


        public static Vehiculos_central operator +(Vehiculos_central c, Vehiculo nuevaLlamada)
        {
            if (nuevaLlamada != null)
            {
                c.AgregarLlamada(nuevaLlamada);
            }
            return c;
        }


        private bool ContieneVehiculo(Vehiculo vehiculo)
        {
            if (vehiculo == null)
            {
                return false;
            }

            if (vehiculo is Auto auto)
            {
                List<Auto> listaExistente = this.manipuladorjsonAuto.Leer<Auto>();
                return listaExistente.Any(v => v.matricula == auto.matricula);
            }
            else if (vehiculo is Camion camion)
            {
                List<Camion> listaExistente = this.manipuladorjsonCamion.Leer<Camion>();
                return listaExistente.Any(v => v.matricula == camion.matricula);
            }
            if (vehiculo is Deportivo deportivo)
            {
                List<Deportivo> listaExistente = this.manipuladorjsonDeportivo.Leer<Deportivo>();
                return listaExistente.Any(v => v.matricula == deportivo.matricula);
            }
            if (vehiculo is Moto moto)
            {
                List<Moto> listaExistente = this.manipuladorjsonMoto.Leer<Moto>();
                return listaExistente.Any(v => v.matricula == moto.matricula);
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



        /// <summary>
        /// Compara si un vehículo está contenido en la centralita.
        /// </summary>
        /// <param name="c">La instancia de Vehiculos_central.</param>
        /// <param name="nuevaLlamada">El vehículo a comparar.</param>
        /// <returns>True si el vehículo está contenido, false en caso contrario.</returns>
        public static bool operator ==(Vehiculos_central c, Vehiculo nuevaLlamada)
        {
            return c.ContieneVehiculo(nuevaLlamada);
        }

        public static bool operator !=(Vehiculos_central c, Vehiculo nuevaLlamada)
        {
            return !c.ContieneVehiculo(nuevaLlamada);
        }


        public override bool Equals(object obj)
        {
            return Equals(obj as Vehiculos_central);
        }

        public bool Equals(Vehiculos_central other)
        {
            return !(other is null) &&
                    EqualityComparer<List<Vehiculo>>.Default.Equals(listaDeLLamadas, other.listaDeLLamadas) &&
                    currentDirectory2 == other.currentDirectory2 &&
                    EqualityComparer<List<Vehiculo>>.Default.Equals(Llamadas, other.Llamadas);
        }

        public override int GetHashCode()
        {
            int hashCode = -273525045;
            hashCode = hashCode * -1521134295 + EqualityComparer<List<Vehiculo>>.Default.GetHashCode(listaDeLLamadas);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(currentDirectory2);
            hashCode = hashCode * -1521134295 + EqualityComparer<List<Vehiculo>>.Default.GetHashCode(Llamadas);
            return hashCode;
        }
    }
}
