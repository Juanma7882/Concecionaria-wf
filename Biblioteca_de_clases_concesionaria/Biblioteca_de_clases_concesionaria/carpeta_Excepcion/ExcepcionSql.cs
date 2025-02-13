using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca_de_clases_concesionaria.carpeta_Excepcion
{
    internal class ExcepcionSql:Exception
    {
        public ExcepcionSql() : base("Este dato es nulo.")
        {
        }

        public ExcepcionSql(string message) : base(message)
        {
        }
        public ExcepcionSql(string mensaje, ExcepcionSql message) : base(mensaje, message)
        {
        }

        public ExcepcionSql(string message, Exception innerException) : base(message, innerException)
        {
        }

    }
}
