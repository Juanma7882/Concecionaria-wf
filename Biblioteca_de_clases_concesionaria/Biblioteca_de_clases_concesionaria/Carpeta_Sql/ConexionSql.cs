using Biblioteca_de_clases_concesionaria.carpeta_Excepcion;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca_de_clases_concesionaria.Carpeta_Sql
{
    internal class ConexionSql
    {
        private SqlConnection connection;
        private static ConexionSql instance;
        private string serverName;
        private string databaseName;
        private string masterConnectionString;

        private ConexionSql(string Nombre_de_la_bd, string Server_name)
        {
            this.serverName = Server_name;
            this.databaseName = Nombre_de_la_bd;
            CrearBD();
        }

        public static ConexionSql GetInstance(string Nombre_de_la_bd, string Server_name)
        {
            if (instance == null)
            {
                instance = new ConexionSql(Nombre_de_la_bd, Server_name);
            }
            return instance;
        }

        private void CrearBD()
        {
            masterConnectionString = $"Data Source={serverName};Initial Catalog=master;Integrated Security=True;";
            using (SqlConnection masterConnection = new SqlConnection(masterConnectionString))
            {
                try
                {
                    masterConnection.Open();

                    string checkDbQuery = $"IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'{databaseName}') BEGIN CREATE DATABASE [{databaseName}] END";

                    using (SqlCommand command = new SqlCommand(checkDbQuery, masterConnection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    //Console.WriteLine($"Error ensuring database exists: {ex.Message}");
                    throw new ExcepcionSql(ex.Message);
                }
            }
        }

        private SqlConnection Connection()
        {
            if (connection == null)
            {
                connection = new SqlConnection($"Data Source={serverName};Initial Catalog={databaseName};Integrated Security=True;");
            }
            return connection;
        }

        public SqlConnection GetConnection()
        {
            Connection();
            return this.connection;
        }
    }
}
