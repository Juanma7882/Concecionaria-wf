using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca_de_clases_concesionaria.carpeta_Excepcion
{
    public class ExcepcionJson: Exception
    {
        public ExcepcionJson():base("ocurrio un Error en el json") { }
        public ExcepcionJson(string message) : base(message)
        {
        }

        public ExcepcionJson(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
