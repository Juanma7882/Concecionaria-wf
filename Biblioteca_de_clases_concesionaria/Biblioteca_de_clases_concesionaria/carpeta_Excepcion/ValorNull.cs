using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca_de_clases_concesionaria.carpeta_Excepcion
{
    internal class ValorNull : Exception
    {
        public ValorNull() : base("Este dato es nulo.")
        {
        }

        public ValorNull(string message) : base(message)
        {
        }

        public ValorNull(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
