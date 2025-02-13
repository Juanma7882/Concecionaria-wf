using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Biblioteca_de_clases_concesionaria.Cvehiculos
{
    public class Deportivo : Auto, IEquatable<Deportivo>
    {
       
        public Deportivo()
        {
        }

        public Deportivo(string MODELO, int ANO, int KILOMETRAJE, string MOTOR, int PRECIO, int PUERTAS, int CANTIDADdePASAJEROS, string MATRICULA)
        : base(MODELO, ANO, KILOMETRAJE, MOTOR, PRECIO, PUERTAS, CANTIDADdePASAJEROS, MATRICULA)
        {
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


        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Camion other = (Camion)obj;
            return base.Equals(other) &&
                    modelo == other.modelo;
        }


        public bool Equals(Deportivo other)
        {
            return !(other is null) &&
                    base.Equals(other) &&
                    modelo == other.modelo &&
                    ano == other.ano &&
                    kilometraje == other.kilometraje &&
                    motor == other.motor &&
                    precio == other.precio &&
                    puertas == other.puertas &&
                    cantidadDePasajeros == other.cantidadDePasajeros &&
                    matricula == other.matricula;
        }


        public override int GetHashCode()
        {
            int hashCode = 702171155;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(modelo);
            hashCode = hashCode * -1521134295 + ano.GetHashCode();
            hashCode = hashCode * -1521134295 + kilometraje.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(motor);
            hashCode = hashCode * -1521134295 + precio.GetHashCode();
            hashCode = hashCode * -1521134295 + puertas.GetHashCode();
            hashCode = hashCode * -1521134295 + cantidadDePasajeros.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(matricula);
            return hashCode;
        }


        public static bool operator ==(Deportivo left, Deportivo right)
        {
            return EqualityComparer<Deportivo>.Default.Equals(left, right);
        }


        public static bool operator !=(Deportivo left, Deportivo right)
        {
            return !(left == right);
        }
    }
}
