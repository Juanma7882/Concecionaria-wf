using Biblioteca_de_clases_concesionaria.Cvehiculos;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Biblioteca_de_clases_concesionaria.Cvehiculos.Vehiculo;



namespace Biblioteca_de_clases_concesionaria
{
    // delegado 
    public delegate void  MostrarDatosHandler(object sender,EventArgs e);

    public class Administrador : Persona
    {
        //Evento
        public event MostrarDatosHandler MostrarDatos;
        DaoVehiculosSql daoVehiculosSql;

        public Administrador(){ daoVehiculosSql = new DaoVehiculosSql();}

        public Administrador(string Nombre, string Apellido, int Dni, string Usuario, string Contrasena) : base(Nombre, Apellido, Dni, Usuario, Contrasena)
        {
            daoVehiculosSql = new DaoVehiculosSql();    
        }


        protected override string Mostrar()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(base.Mostrar());
            return sb.ToString();
        }


        // Método público para activar el evento
        public virtual string OnMostrar()
        {
            // Invoca el evento si no es null
            MostrarDatos?.Invoke(this, EventArgs.Empty);
            return Mostrar();
        }

        public void EliminarVehiculo(string Matricula, Tipovehiculo tipovehiculo)
        {
            DaoVehiculosSql daoVehiculos = new DaoVehiculosSql();
            if (Tipovehiculo.Auto.ToString() == "Deportivo")
            {
                if (!string.IsNullOrEmpty(Matricula))
                {
                    daoVehiculos -= Matricula;
                }
            }
            else if ("Moto" == Tipovehiculo.Moto.ToString())
            {
                if (!string.IsNullOrEmpty(Matricula))
                {
                    this.daoVehiculosSql -= Matricula;
                }

            }
            else if ("Camion" == Tipovehiculo.Camion.ToString())
            {
                if (!string.IsNullOrEmpty(Matricula))
                {
                    this.daoVehiculosSql -= Matricula;
                }

            }
            else if ("Auto" == Tipovehiculo.Deportivo.ToString())
            {
                if (!string.IsNullOrEmpty(Matricula))
                {
                    this.daoVehiculosSql -= Matricula;
                }
            }
        }

        public bool ValidarMatriculas(string TipoVehiculo, string matricula)
        {
            bool validarMatriculasBandera = false;
            if (TipoVehiculo == "Auto")
            {
                Auto auto = new Auto();
                return validarMatriculasBandera = auto.Matricula(matricula);
            }
            else if (TipoVehiculo == "Deportivo")
            {
                Deportivo deportivo = new Deportivo();
                return validarMatriculasBandera = deportivo.Matricula(matricula);
            }
            else if (TipoVehiculo == "Camion")
            {
                Camion camion = new Camion();
                return validarMatriculasBandera = camion.Matricula(matricula);
            }
            else if (TipoVehiculo == "Moto")
            {
                Moto moto = new Moto();
                return validarMatriculasBandera = moto.Matricula(matricula);
            }
           
            return validarMatriculasBandera;
        }




        public bool AgregarVeiculo(Vehiculo vehiculo)
        {
            bool resultado = daoVehiculosSql == vehiculo;
            if (vehiculo != null && resultado == false)
            {
                daoVehiculosSql += vehiculo;
                return true;
            }
            return false;
        }

        public string ModificarVehiculo(string oldmatricula, Vehiculo vehiculo,string newMatricula)
        {
            string mensaje = "Verifique que vehiculo o la matricula no este vacio";
            bool resultado = daoVehiculosSql == vehiculo;
            bool resultado2 = daoVehiculosSql.Buscar(newMatricula);

            if (oldmatricula == newMatricula)
            {
                daoVehiculosSql.ActualizarVehiculo(vehiculo);
                mensaje = "modificasion exitosa";
            }
            else if (resultado2)
            {
                mensaje = "esta matricula ya existe revise nuevamente";
            }
            else
            {
                daoVehiculosSql -= oldmatricula;
                daoVehiculosSql += vehiculo;
                mensaje = "Modificasion Exitosa"; 
            }
            
            return mensaje;
        }
        //else if (oldmatricula != null && vehiculo != null)








        public List<Deportivo> LeerDeportivo(string ordenarPor, bool acendente_decendente = true)
        {
            try
            {
                Vehiculos_central vehiculos_Central = new Vehiculos_central();
                if (ordenarPor != null)
                {
                    List<Deportivo> listaDeportivos = this.daoVehiculosSql.LeerDeportivos();
                    List<Deportivo> deportivosOrdenados = vehiculos_Central.Ordenar_por_dato(ordenarPor, listaDeportivos, acendente_decendente);
                    return deportivosOrdenados;
                }
                else
                {
                    throw new ArgumentException("Tipo de vehículo no reconocido");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Se produjo un error al leer el archivo JSON: {ex.Message}\n");
            }
        }


        public List<Auto> LeerAuto(string ordenarPor, bool acendente_decendente = true)
        {
            try
            {
                Vehiculos_central vehiculos_Central = new Vehiculos_central();
                if (ordenarPor != null)
                {
                    List<Auto> listaAutos = this.daoVehiculosSql.LeerAuto();
                    List<Auto> autosOrdenados = vehiculos_Central.Ordenar_por_dato(ordenarPor, listaAutos, acendente_decendente);
                    return autosOrdenados;
                }
                else
                {
                    throw new ArgumentException("Tipo de vehículo no reconocido");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Se produjo un error al leer el archivo JSON: {ex.Message}\n");
            }
        }


        public List<Moto> LeerMoto(string ordenarPor, bool acendente_decendente = true)
        {
            try
            {
                Vehiculos_central vehiculos_Central = new Vehiculos_central();
                if (ordenarPor != null)
                {
                    List<Moto> listaMotos = this.daoVehiculosSql.LeerMotos();
                    List<Moto> motosOrdenadas = vehiculos_Central.Ordenar_por_dato(ordenarPor, listaMotos, acendente_decendente);
                    return motosOrdenadas;
                }
                else
                {
                    throw new ArgumentException("Tipo de vehículo no reconocido");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Se produjo un error al leer el archivo JSON: {ex.Message}\n");
            }
        }


        public List<Camion> LeerCamion(string ordenarPor, bool acendente_decendente = true)
        {
            try
            {
                Vehiculos_central vehiculos_Central = new Vehiculos_central();
                if (ordenarPor != null)
                {
                    List<Camion> listaCamiones = this.daoVehiculosSql.LeerCamiones();
                    List<Camion> camionesOrdenados = vehiculos_Central.Ordenar_por_dato(ordenarPor, listaCamiones, acendente_decendente);
                    return camionesOrdenados;
                }
                else
                {
                    throw new ArgumentException("Tipo de vehículo no reconocido");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Se produjo un error al leer el archivo JSON: {ex.Message}\n");
            }
        }

    }
}
