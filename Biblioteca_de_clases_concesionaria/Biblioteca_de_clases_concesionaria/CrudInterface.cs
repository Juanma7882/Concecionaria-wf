using System.Collections.Generic;

namespace Biblioteca_de_clases_concesionaria
{
    public interface ICrud
    {

        string Crear();
        string Escribir<T>(List<T> lista);
        List<T> Leer<T>();
        string BorrarUnaClase<T>(string buscar, int nombreEmpleado);
    }
}
