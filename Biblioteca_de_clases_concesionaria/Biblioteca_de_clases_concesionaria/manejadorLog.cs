using Biblioteca_de_clases_concesionaria.carpeta_Excepcion;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca_de_clases_concesionaria
{
    public class ManejadorLog
    {
        private readonly string path;
        private readonly string Nombre;
        private readonly string RutaCompleta;
        public ManejadorLog()
        {
            this.path = @"..\..\..\Aplicasion_WF_Consecionaria\archivoslog\";
            this.Nombre = "usuarios.log";
            this.RutaCompleta = Path.Combine(path, Nombre);
        }

        private string CrearLog()
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                if (!File.Exists(RutaCompleta))
                {
                    File.Create(RutaCompleta).Close();
                    return $"Archivo de log creado con éxito en: {RutaCompleta}\n";
                }
                return "El archivo de log ya existe.\n";
            }
            catch (Exception ex)
            {
                throw new Excepcionlog(ex.Message);
            }
        }


        public string EscribirLog(string fechaHora, string texto)
        {
            try
            {
                CrearLog();
                using (StreamWriter sw = new StreamWriter(RutaCompleta, true))
                {
                    string logEntry = $"{fechaHora} : {texto}\n";
                    sw.WriteLine(logEntry);
                }
                return $"Entrada de log añadida con éxito en: {RutaCompleta}\n";
            }
            catch (Exception ex)
            {
                throw new Excepcionlog(ex.Message);

            }
        }


        public string EscribirLog(string texto)
        {
            try
            {
                DateTime fechaActual = DateTime.Now;
                string time = fechaActual.ToString("dd/MM/yyyy hh:mm:ss tt");
                ManejadorLog manejadorLog = new ManejadorLog();
                manejadorLog.EscribirLog(time, texto);
                return EscribirLog(time, texto);
            }
            catch (Exception ex)
            {
                throw new Excepcionlog(ex.Message);
            }
        }

        public string EscribirLog()
        {
            try
            {
                string texto = "vacio";
                DateTime fechaActual = DateTime.Now;
                string time = fechaActual.ToString("dd/MM/yyyy hh:mm:ss tt");
                ManejadorLog manejadorLog = new ManejadorLog();
                manejadorLog.EscribirLog(time, texto);
                return EscribirLog(time, texto);
            }
            catch (Exception ex)
            {
                throw new Excepcionlog(ex.Message);
            }
        }

        public string LeerLog()
        {
            try
            {
                CrearLog();
                using (StreamReader sr = new StreamReader(RutaCompleta))
                {
                    return sr.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                throw new Excepcionlog(ex.Message);
            }
        }

    }
}
