//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Text.Json.Serialization;
//using System.Threading.Tasks;


//namespace Biblioteca_de_clases_concesionaria
//{
//    internal class serealizador
//    {
//        public class Serializar<T>
//        {
//            public static void SerializarLista(List<T> lista, string path)
//            {
//                try
//                {
//                    string json = JsonConvert.SerializeObject(lista, Newtonsoft.Json.Formatting.Indented);
//                    File.WriteAllText(path, json);
//                }
//                catch (Exception e)
//                {
//                    Console.WriteLine(e.ToString());
//                }

//            }

//            public static List<T> DeserializarLista(string nombreArchivo)
//            {
//                string path = Environment.CurrentDirectory;

//                string filePath = Path.Combine(path, nombreArchivo);


//                Console.WriteLine(filePath);

//                List<T> lista = new List<T>();

//                try
//                {
//                    string json = File.ReadAllText(filePath);    //lee todo el archivo
//                    lista = JsonConvert.DeserializeObject<List<T>>(json); // lo deserializa

//                }
//                catch (Exception e)
//                {
//                    Console.WriteLine(e.ToString());
//                }

//                return lista;
//            }
//        }
//    }
//}
