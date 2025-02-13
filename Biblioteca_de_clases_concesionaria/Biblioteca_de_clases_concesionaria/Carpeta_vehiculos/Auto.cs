using Biblioteca_de_clases_concesionaria;
using Biblioteca_de_clases_concesionaria.Cvehiculos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Biblioteca_de_clases_concesionaria
{

    //// delegado 
    public delegate void MostrarDatosHandleraAuto(object sender, EventArgs e);

    public class Auto : Vehiculo, IEquatable<Auto>
    {
        public int puertas { get; set; }
        public int cantidadDePasajeros { get; set; }
        public string matricula { get; set; }

        //Evento
        public event MostrarDatosHandleraAuto MostrarDatos;

        public Auto()
        {
        }

        private Auto(string MODELO, int ANO, int KILOMETRAJE, string MOTOR, int PRECIO)
            : base(MODELO, ANO, KILOMETRAJE, MOTOR, PRECIO)
        {
        }

        public Auto(string MODELO, int ANO, int KILOMETRAJE, string MOTOR, int PRECIO, int PUERTAS, int CANTIDADdePASAJEROS, string MATRICULA)
        : base(MODELO, ANO, KILOMETRAJE, MOTOR, PRECIO)
        {
            this.puertas = PUERTAS;
            this.cantidadDePasajeros = CANTIDADdePASAJEROS;
            this.matricula = MATRICULA;
        }

        public override bool Matricula(string validar)
        {
            if (validar != null)
            {
                string validadorMatriculaAuto = @"^[a-zA-Z]{3}\d{3}$";
                return Regex.IsMatch(validar, validadorMatriculaAuto);
            }
            return false;
        }

        public override string Mostrar()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"PUERTAS :{this.puertas}");
            sb.AppendLine($"CANTIDAD DE PASAJEROS : {this.cantidadDePasajeros}");
            sb.AppendLine($"MATRICULA : {this.matricula}");
            return sb.ToString();
        }


        //Método público para activar el evento
        public virtual string OnMostrar()
        {
            // Invoca el evento si no es null
            MostrarDatos?.Invoke(this, EventArgs.Empty);
            return Mostrar();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            Auto clase = (Auto)obj;
            return base.Equals(clase) &&
                    puertas == clase.puertas &&
                    cantidadDePasajeros == clase.cantidadDePasajeros &&
                    matricula == clase.matricula;
        }

        public bool Equals(Auto other)
        {
            return !(other is null) &&
                    base.Equals(other) &&
                    modelo == other.modelo &&
                    ano == other.ano &&
                    kilometraje == other.kilometraje &&
                    motor == other.motor &&
                    puertas == other.puertas &&
                    cantidadDePasajeros == other.cantidadDePasajeros &&
                    matricula == other.matricula;
        }

        public override int GetHashCode()
        {
            int hashCode = -1502864396;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(modelo);
            hashCode = hashCode * -1521134295 + ano.GetHashCode();
            hashCode = hashCode * -1521134295 + kilometraje.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(motor);
            hashCode = hashCode * -1521134295 + puertas.GetHashCode();
            hashCode = hashCode * -1521134295 + cantidadDePasajeros.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(matricula);
            return hashCode;
        }

        public static bool operator ==(Auto left, Auto right)
        {
            return EqualityComparer<Auto>.Default.Equals(left, right);
        }

        public static bool operator !=(Auto left, Auto right)
        {
            return !(left == right);
        }
            
            
    }
}






//// delegado 
//public delegate void MostrarDatosHandler(object sender, EventArgs e);

//public class Administrador : Persona
//{
//    //Evento
//    public event MostrarDatosHandler MostrarDatos;

//    public Administrador(string Nombre, string Apellido, int Dni, string Usuario, string Contrasena) : base(Nombre, Apellido, Dni, Usuario, Contrasena)
//    {
//    }


//    protected override string Mostrar()
//    {
//        StringBuilder sb = new StringBuilder();
//        sb.AppendLine(base.Mostrar());
//        return sb.ToString();
//    }


//    // Método público para activar el evento
//    public virtual string OnMostrar()
//    {
//        // Invoca el evento si no es null
//        MostrarDatos?.Invoke(this, EventArgs.Empty);
//        return Mostrar();

//    }