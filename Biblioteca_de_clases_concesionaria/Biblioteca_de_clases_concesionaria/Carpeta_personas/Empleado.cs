using Biblioteca_de_clases_concesionaria.Cvehiculos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Biblioteca_de_clases_concesionaria.Cvehiculos.Vehiculo;

namespace Biblioteca_de_clases_concesionaria
{
    public class Empleado : Persona
    {

        DaoVehiculosSql daoVehiculosSql;

        public Empleado() { daoVehiculosSql = new DaoVehiculosSql(); }


        public Empleado(string Nombre, string Apellido, int Dni, string Usuario, string Contrasena) : base(Nombre, Apellido, Dni, Usuario, Contrasena)
        {
            daoVehiculosSql = new DaoVehiculosSql();
        }

        protected override string Mostrar()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(base.Mostrar());
            sb.AppendLine($"colocar texto a la clase empleado");
            return sb.ToString();
        }

        public override string ToString()
        {
            return Mostrar();
        }

        public bool AgregarVeiculo(Vehiculo vehiculo)
        {
            bool resultado = daoVehiculosSql == vehiculo;
            if(vehiculo != null && resultado == false)
            {
                daoVehiculosSql += vehiculo;
                return true;
            }
            return false;
        }

        public bool ModificarVehiculo(string matricula, Vehiculo vehiculo)
        {
            bool resultado = daoVehiculosSql == vehiculo;

            if (matricula != null && vehiculo != null && resultado == false) 
            {
                daoVehiculosSql -= matricula;
                daoVehiculosSql += vehiculo;
                return true;
            }
            return false;
        }


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


//○ El perfil de administrador pueda realizar el CRUD (Create, Read, Update y Delete) completo.

//○ El perfil de supervisor pueda realizar solamente ‘CRU’ (Create, Read y Update)

//○ El perfil de vendedor solo pueda realizar el ‘R’ (Read).