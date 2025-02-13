using Biblioteca_de_clases_concesionaria.carpeta_Excepcion;
using Biblioteca_de_clases_concesionaria.Carpeta_Sql;
using Biblioteca_de_clases_concesionaria.Cvehiculos;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Xml.Linq;
using static Biblioteca_de_clases_concesionaria.Cvehiculos.Vehiculo;
using Biblioteca_de_clases_concesionaria.Carpeta_personas;
using System.Runtime.CompilerServices;
namespace Biblioteca_de_clases_concesionaria
{
    

    public delegate void PreInicializadorvehiculoEventHandler(object sender, EventArgs e);

    public class DaoVehiculosSql : IEquatable<DaoVehiculosSql>
    {
        public enum TipoOrden
        {
            Precio,
            Matricula,
        }

        private SqlConnection connection;
        public event PreInicializadorvehiculoEventHandler preInicializadorDaoSql;
        private Task task;

        public DaoVehiculosSql()
        {
            ConexionSql BaseDeDatosVehiculos = ConexionSql.GetInstance("Vehiculos_db", "DESKTOP-51LFFAI\\SQLEXPRESS");
            this.connection = BaseDeDatosVehiculos.GetConnection();
            if (this.connection == null)
            {
                throw new ValorNull();
            }
            //Tabla_tipos();
        }


        public void PreInicializadorDaoSql()
        {
            // Invoca el evento si no es null
            preInicializadorDaoSql?.Invoke(this, EventArgs.Empty);
            // Realiza operaciones en el hilo de fondo
            Task.Run(() =>
            {
                DaoVehiculosSql vehiculos = new DaoVehiculosSql();
                CreartablaVehiculos();
            }
            );
        }

        private void CreartablaVehiculos()
        {
            if (this.connection == null)
            {
                throw new ValorNull();
            }
            string crearTablaQuery = @" IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Vehiculos' and xtype='U')
                                        CREATE TABLE Vehiculos(
                                        ID_VEHICULO INT IDENTITY(1,1) PRIMARY KEY,
                                        MODELO VARCHAR(50) NOT NULL,
                                        ANO INT NOT NULL ,
                                        KILOMETRAJE int NOT NULL,
                                        MOTOR VARCHAR(100) NOT NULL,                                        
                                        PRECIO INT  NOT NULL,
                                        TIPO_VEHICULO VARCHAR(100) NOT NULL,
                                        MATRICULA VARCHAR(100) NOT NULL UNIQUE, 
                                        PUERTAS INT ,
                                        CANTIDAD_DE_PASAJEROS INT ,
                                        TORQUE FLOAT ,
                                        CARGAMAX FLOAT ,
                                        CILINDRADA INT ,
                                        PESO INT   
                                        )";
            try
            {
                this.connection.Open();
                using (SqlCommand command = new SqlCommand(crearTablaQuery, this.connection))
                {
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new ExcepcionSql(ex.Message);
            }
            finally
            {
                if (this.connection != null && this.connection.State == System.Data.ConnectionState.Open)
                {
                    this.connection.Close();
                }
            }
        }


        private int AgregarVehiculo(Vehiculo vehiculo)
        {
            int idVehiculo;
            string tipo_vehiculo;
            string insertVehiculoQuery = @"INSERT INTO Vehiculos (MODELO, ANO, KILOMETRAJE, MOTOR, PRECIO,  TIPO_VEHICULO,MATRICULA,PUERTAS,CANTIDAD_DE_PASAJEROS,TORQUE,CARGAMAX,CILINDRADA,PESO )
                                            VALUES (@MODELO, @ANO, @KILOMETRAJE, @MOTOR, @PRECIO, @TIPO_VEHICULO , @MATRICULA , @PUERTAS, @CANTIDAD_DE_PASAJEROS, @TORQUE, @CARGAMAX , @CILINDRADA, @PESO );
                                            SELECT SCOPE_IDENTITY();";
            try
            {
                this.connection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(insertVehiculoQuery, this.connection))
                {
                    sqlCommand.Parameters.AddWithValue("@MODELO", vehiculo.modelo);
                    sqlCommand.Parameters.AddWithValue("@ANO", vehiculo.ano);
                    sqlCommand.Parameters.AddWithValue("@KILOMETRAJE", vehiculo.kilometraje);
                    sqlCommand.Parameters.AddWithValue("@MOTOR", vehiculo.motor);
                    sqlCommand.Parameters.AddWithValue("@PRECIO", vehiculo.precio);
                    sqlCommand.Parameters.AddWithValue("@PUERTAS", (object)DBNull.Value);
                    sqlCommand.Parameters.AddWithValue("@CANTIDAD_DE_PASAJEROS", (object)DBNull.Value);
                    sqlCommand.Parameters.AddWithValue("@TORQUE", (object)DBNull.Value);
                    sqlCommand.Parameters.AddWithValue("@CARGAMAX", (object)DBNull.Value);
                    sqlCommand.Parameters.AddWithValue("@CILINDRADA", (object)DBNull.Value);
                    sqlCommand.Parameters.AddWithValue("@PESO", (object)DBNull.Value);

                    // Parámetros específicos según el tipo de vehículo
                    if (vehiculo is Deportivo deportivo)
                    {
                        tipo_vehiculo = "Deportivo";
                        sqlCommand.Parameters.AddWithValue("@TIPO_VEHICULO", tipo_vehiculo);
                        sqlCommand.Parameters.AddWithValue("@MATRICULA", deportivo.matricula);
                        sqlCommand.Parameters["@PUERTAS"].Value = deportivo.puertas;
                        sqlCommand.Parameters["@CANTIDAD_DE_PASAJEROS"].Value = deportivo.cantidadDePasajeros;
                    }
                    else if (vehiculo is Auto auto)
                    {
                        tipo_vehiculo = "Auto";
                        sqlCommand.Parameters.AddWithValue("@TIPO_VEHICULO", tipo_vehiculo);
                        sqlCommand.Parameters.AddWithValue("@MATRICULA", auto.matricula);
                        sqlCommand.Parameters["@PUERTAS"].Value = auto.puertas;
                        sqlCommand.Parameters["@CANTIDAD_DE_PASAJEROS"].Value = auto.cantidadDePasajeros;
                    }
                    else if (vehiculo is Moto moto)
                    {
                        tipo_vehiculo = "Moto";
                        sqlCommand.Parameters.AddWithValue("@TIPO_VEHICULO", tipo_vehiculo);
                        sqlCommand.Parameters.AddWithValue("@MATRICULA", moto.matricula);
                        sqlCommand.Parameters["@CILINDRADA"].Value = moto.cilindrada;
                        sqlCommand.Parameters["@PESO"].Value = moto.peso;
                    }
                    else if (vehiculo is Camion camion)
                    {
                        tipo_vehiculo = "Camion";
                        sqlCommand.Parameters.AddWithValue("@TIPO_VEHICULO", tipo_vehiculo);
                        sqlCommand.Parameters.AddWithValue("@MATRICULA", camion.matricula);
                        sqlCommand.Parameters["@TORQUE"].Value = camion.torque;
                        sqlCommand.Parameters["@CARGAMAX"].Value = camion.cargamax;
                    }
                   

                    idVehiculo = Convert.ToInt32(sqlCommand.ExecuteScalar());
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627) // Código de error para duplicados
                {
                    throw new ExcepcionSql($"Este Dni ya se encuentra en la base de Datos");
                }
                else
                {
                    throw new ExcepcionSql(ex.Message);
                }
            }
            catch (Exception ex)
            {
                throw new ExcepcionSql(ex.Message);
            }
            finally
            {
                if (this.connection != null && this.connection.State == System.Data.ConnectionState.Open)
                {
                    this.connection.Close();
                }
            }
            return idVehiculo;
        }


        public string EliminarUsuarioPorMatricula(string matricula)
        {
            try
            {
                this.connection.Open();
                string deleteQuery = "DELETE FROM Vehiculos WHERE MATRICULA = @MATRICULA";
                using (SqlCommand command = new SqlCommand(deleteQuery, this.connection))
                {
                    command.Parameters.AddWithValue("@MATRICULA", matricula);
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return "Registro eliminado con éxito.";
                    }
                    else
                    {
                        return "No se encontró ningún registro con la MATRICULA proporcionado.";
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                this.connection.Close();
            }
        }

        


        public bool ActualizarVehiculo(Vehiculo vehiculo)
        {
            string updateVehiculoQuery = @"UPDATE Vehiculos
            SET MODELO = @MODELO, ANO = @ANO, KILOMETRAJE = @KILOMETRAJE, MOTOR = @MOTOR, PRECIO = @PRECIO, MATRICULA = @MATRICULA,PUERTAS = @PUERTAS , CANTIDAD_DE_PASAJEROS = @CANTIDAD_DE_PASAJEROS, CILINDRADA = @CILINDRADA,PESO = @PESO, TORQUE = @TORQUE, CARGAMAX = @CARGAMAX 
            WHERE MATRICULA = @MATRICULA";

            try
            {
                this.connection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(updateVehiculoQuery, this.connection))
                {
                    sqlCommand.Parameters.AddWithValue("@MODELO", vehiculo.modelo);
                    sqlCommand.Parameters.AddWithValue("@ANO", vehiculo.ano);
                    sqlCommand.Parameters.AddWithValue("@KILOMETRAJE", vehiculo.kilometraje);
                    sqlCommand.Parameters.AddWithValue("@MOTOR", vehiculo.motor);
                    sqlCommand.Parameters.AddWithValue("@PRECIO", vehiculo.precio);

                    // Asignar valores predeterminados para los parámetros opcionales
                    sqlCommand.Parameters.AddWithValue("@PUERTAS", (object)DBNull.Value);
                    sqlCommand.Parameters.AddWithValue("@CANTIDAD_DE_PASAJEROS", (object)DBNull.Value);
                    sqlCommand.Parameters.AddWithValue("@CILINDRADA", (object)DBNull.Value);
                    sqlCommand.Parameters.AddWithValue("@PESO", (object)DBNull.Value);
                    sqlCommand.Parameters.AddWithValue("@TORQUE", (object)DBNull.Value);
                    sqlCommand.Parameters.AddWithValue("@CARGAMAX", (object)DBNull.Value);

                    // Parámetros específicos según el tipo de vehículo
                    if (vehiculo is Deportivo deportivo)
                    {
                        sqlCommand.Parameters.AddWithValue("@MATRICULA", deportivo.matricula);
                        sqlCommand.Parameters["@PUERTAS"].Value = deportivo.puertas;
                        sqlCommand.Parameters["@CANTIDAD_DE_PASAJEROS"].Value = deportivo.cantidadDePasajeros;
                    }
                    else if (vehiculo is Auto auto)
                    {
                        sqlCommand.Parameters.AddWithValue("@MATRICULA", auto.matricula);
                        sqlCommand.Parameters["@PUERTAS"].Value = auto.puertas;
                        sqlCommand.Parameters["@CANTIDAD_DE_PASAJEROS"].Value = auto.cantidadDePasajeros;
                    }
                    else if (vehiculo is Moto moto)
                    {
                        sqlCommand.Parameters.AddWithValue("@MATRICULA", moto.matricula);
                        sqlCommand.Parameters["@CILINDRADA"].Value = moto.cilindrada;
                        sqlCommand.Parameters["@PESO"].Value = moto.peso;
                    }
                    else if (vehiculo is Camion camion)
                    {
                        sqlCommand.Parameters.AddWithValue("@MATRICULA", camion.matricula);
                        sqlCommand.Parameters["@TORQUE"].Value = camion.torque;
                        sqlCommand.Parameters["@CARGAMAX"].Value = camion.cargamax;
                    }

                    int rowsAffected = sqlCommand.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                throw new ExcepcionSql(ex.Message);
            }
            finally
            {
                if (this.connection != null && this.connection.State == System.Data.ConnectionState.Open)
                {
                    this.connection.Close();
                }
            }
        }

        public List<Auto> LeerAuto()
        {
            List<Auto> autos = new List<Auto>();

            string query = @"
            SELECT  MODELO, ANO, KILOMETRAJE, MOTOR, PRECIO,
                    PUERTAS, CANTIDAD_DE_PASAJEROS, MATRICULA
            FROM Vehiculos 
            WHERE TIPO_VEHICULO = 'Auto'";

            try
            {
                this.connection.Open();
                using (SqlCommand command = new SqlCommand(query, this.connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string modelo = reader["MODELO"].ToString();
                            int ano = Convert.ToInt32(reader["ANO"]);
                            int kilometraje = Convert.ToInt32(reader["KILOMETRAJE"]);
                            string motor = reader["MOTOR"].ToString();
                            int precio = Convert.ToInt32(reader["PRECIO"]);
                            int puertas = Convert.ToInt32(reader["PUERTAS"]);
                            int cantidadDePasajeros = Convert.ToInt32(reader["CANTIDAD_DE_PASAJEROS"]);
                            string matricula = reader["MATRICULA"].ToString();
                            Auto auto = new Auto(modelo, ano, kilometraje, motor, precio, puertas, cantidadDePasajeros, matricula);
                            autos.Add(auto);
                        }
                    }
                    this.connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new ExcepcionSql(ex.Message);
            }
            finally
            {
                this.connection.Close();
            }

            return autos;
        }

        public List<Moto> LeerMotos()
        {
            List<Moto> motos = new List<Moto>();

            string query = @"
            SELECT MODELO, ANO, KILOMETRAJE, MOTOR, PRECIO,
                    CILINDRADA, PESO, MATRICULA
            FROM Vehiculos 
            WHERE TIPO_VEHICULO = 'Moto'";

            try
            {
                this.connection.Open();
                using (SqlCommand command = new SqlCommand(query, this.connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string modelo = reader["MODELO"].ToString();
                            int ano = Convert.ToInt32(reader["ANO"]);
                            int kilometraje = Convert.ToInt32(reader["KILOMETRAJE"]);
                            string motor = reader["MOTOR"].ToString();
                            int precio = Convert.ToInt32(reader["PRECIO"]);
                            string matricula = reader["MATRICULA"].ToString();
                            int cilindrada = Convert.ToInt32(reader["CILINDRADA"]);
                            int peso = Convert.ToInt32(reader["PESO"]);
                            Moto moto = new Moto(modelo, ano, kilometraje, motor, precio, cilindrada, peso, matricula);
                            motos.Add(moto);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading vehicles: {ex.Message}");
            }
            finally
            {
                this.connection.Close();
            }
            return motos;
        }

        public List<Deportivo> LeerDeportivos()
        {
            List<Deportivo> deportivos = new List<Deportivo>();

            string query = @"
            SELECT MODELO, ANO, KILOMETRAJE, MOTOR, PRECIO,
                    PUERTAS, CANTIDAD_DE_PASAJEROS, MATRICULA
            FROM Vehiculos v
            WHERE TIPO_VEHICULO = 'Deportivo'";

            try
            {
                this.connection.Open();
                using (SqlCommand command = new SqlCommand(query, this.connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string modelo = reader["MODELO"].ToString();
                            int ano = Convert.ToInt32(reader["ANO"]);
                            int kilometraje = Convert.ToInt32(reader["KILOMETRAJE"]);
                            string motor = reader["MOTOR"].ToString();
                            int precio = Convert.ToInt32(reader["PRECIO"]);
                            int puertas = Convert.ToInt32(reader["PUERTAS"]);
                            int cantidadDePasajeros = Convert.ToInt32(reader["CANTIDAD_DE_PASAJEROS"]);
                            string matricula = reader["MATRICULA"].ToString();

                            Deportivo deportivo = new Deportivo(modelo, ano, kilometraje, motor, precio, puertas, cantidadDePasajeros, matricula);
                            deportivos.Add(deportivo);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading vehicles: {ex.Message}");
            }
            finally
            {
                this.connection.Close();
            }

            return deportivos;
        }

        public List<Camion> LeerCamiones()
        {
            List<Camion> camiones = new List<Camion>();

            string query = @"
            SELECT  MODELO, ANO, KILOMETRAJE, MOTOR, PRECIO,
                    CARGAMAX, MATRICULA, TORQUE
            FROM Vehiculos 
            WHERE TIPO_VEHICULO = 'Camion'";

            try
            {
                this.connection.Open();
                using (SqlCommand command = new SqlCommand(query, this.connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string modelo = reader["MODELO"].ToString();
                            int ano = Convert.ToInt32(reader["ANO"]);
                            int kilometraje = Convert.ToInt32(reader["KILOMETRAJE"]);
                            string motor = reader["MOTOR"].ToString();
                            int precio = Convert.ToInt32(reader["PRECIO"]);
                            float torque = Convert.ToInt32(reader["TORQUE"]);
                            float cargaMaxima = Convert.ToSingle(reader["CARGAMAX"]);
                            string matricula = reader["MATRICULA"].ToString();

                            Camion camion = new Camion(modelo, ano, kilometraje, motor, precio, torque, cargaMaxima, matricula);
                            camiones.Add(camion);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ExcepcionSql(ex.Message);
            }
            finally
            {
                this.connection.Close();
            }
            return camiones;
        }

        public bool Buscar(string matricula)
        {
            bool boleano;
            try
            {
                this.connection.Open();
                string query = "SELECT COUNT(*) FROM Vehiculos WHERE MATRICULA = @MATRICULA";
                using (SqlCommand command = new SqlCommand(query, this.connection))
                {
                    command.Parameters.AddWithValue("@MATRICULA", matricula);
                    int count = (int)command.ExecuteScalar();

                    boleano = count > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                this.connection.Close();
            }
            return boleano;
        }


        private bool Buscar(Vehiculo vehiculo)
        {
            if (vehiculo is Deportivo deportivo)
            {
                List<Deportivo> listaExistente = this.LeerDeportivos();
                return listaExistente.Any(v => v.matricula == deportivo.matricula);
            }
            else if (vehiculo is Auto auto)
            {
                List<Auto> listaExistente = this.LeerAuto();
                return listaExistente.Any(v => v.matricula == auto.matricula);
            }
            else if (vehiculo is Moto moto)
            {
                List<Moto> listaExistente = this.LeerMotos();
                return listaExistente.Any(v => v.matricula == moto.matricula);
            }
            else if (vehiculo is Camion camion)
            {
                List<Camion> listaExistente = this.LeerCamiones();
                return listaExistente.Any(v => v.matricula == camion.matricula);
            }
            return false;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as DaoVehiculosSql);
        }

        public bool Equals(DaoVehiculosSql other)
        {
            return !(other is null) &&
                   EqualityComparer<SqlConnection>.Default.Equals(connection, other.connection) &&
                   EqualityComparer<Task>.Default.Equals(task, other.task);
        }

        public override int GetHashCode()
        {
            int hashCode = 1187907117;
            hashCode = hashCode * -1521134295 + EqualityComparer<SqlConnection>.Default.GetHashCode(connection);
            hashCode = hashCode * -1521134295 + EqualityComparer<Task>.Default.GetHashCode(task);
            return hashCode;
        }

        public static DaoVehiculosSql operator +(DaoVehiculosSql daoVehiculosSql, Vehiculo vehiculo)
        {
            int id = daoVehiculosSql.AgregarVehiculo(vehiculo);
            return daoVehiculosSql;
        }

       


        public static DaoVehiculosSql operator -(DaoVehiculosSql daoVehiculosSql, string matricula)
        {
            daoVehiculosSql.EliminarUsuarioPorMatricula(matricula);
            return daoVehiculosSql;
        }


        public static bool operator ==(DaoVehiculosSql daoVehiculosSql, Vehiculo matricula)
        {
          
            return daoVehiculosSql.Buscar(matricula);
        }

        public static bool operator !=(DaoVehiculosSql daoVehiculosSql, Vehiculo matricula)
        {
            if (daoVehiculosSql is null)
                return false;
            return (!daoVehiculosSql.Buscar(matricula));
        }

        public static bool operator ==(DaoVehiculosSql left, DaoVehiculosSql right)
        {
            return EqualityComparer<DaoVehiculosSql>.Default.Equals(left, right);
        }

        public static bool operator !=(DaoVehiculosSql left, DaoVehiculosSql right)
        {
            return !(left == right);
        }
    }
}
