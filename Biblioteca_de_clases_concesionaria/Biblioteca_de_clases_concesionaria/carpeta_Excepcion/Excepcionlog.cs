using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca_de_clases_concesionaria.carpeta_Excepcion
{
    public class Excepcionlog:Exception
    {
        public Excepcionlog() : base("Hubo un error en la clase log")
        {
        }

        public Excepcionlog(string message) : base(message)
        {
        }
        public Excepcionlog(string mensaje, Excepcionlog message) : base(mensaje, message)
        {
        }

        public Excepcionlog(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
