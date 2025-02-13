using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca_de_clases_concesionaria
{
    public abstract class Persona
    {
        public enum TipoPersona
        {
            Administrador,
            Cliente,
            Empleado
        }

        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Dni { get; set; }
        public string Usuario { get; set; }
        public string Contrasena { get; set; }


        //public string nombre
        //{ get { return Nombre; } set { Nombre = value; }}

        public Persona() {}
        public Persona(string Nombre, string Apellido, int Dni, string Usuario, string Contrasena)
        {
            this.Nombre = Nombre;
            this.Apellido = Apellido;
            this.Dni = Dni;
            this.Usuario = Usuario;
            this.Contrasena = Contrasena;
        }

        protected virtual string Mostrar()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($" Nombre: {Nombre} \n");
            sb.AppendLine($" Apellido: {Apellido} \n");
            sb.AppendLine($" Usuario: {Usuario} \n");
            sb.AppendLine($" Dni: {Dni} \n");
            return sb.ToString();
        }

        public override string ToString()
        {
            return Mostrar();
        }


    }
}
