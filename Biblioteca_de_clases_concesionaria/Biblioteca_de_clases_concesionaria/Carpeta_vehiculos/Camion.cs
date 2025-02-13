using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Biblioteca_de_clases_concesionaria.Cvehiculos
{
    public class Camion : Vehiculo, IEquatable<Camion>
    {
        public float torque { get; set; }
        public float cargamax { get; set; }
        public string matricula { get; set; }


        public Camion()
        {
        }

        private Camion(string MODELO, int ANO, int KILOMETRAJE, string MOTOR, int PRECIO)
            : base(MODELO, ANO, KILOMETRAJE, MOTOR, PRECIO)
        {
        }

        public Camion(string MODELO, int ano, int KILOMETRAJE, string MOTOR, int PRECIO, float Torque, float Cargamax, string MATRICULA)
            : base(MODELO, ano, KILOMETRAJE, MOTOR, PRECIO)
        {
            this.torque = Torque;
            this.cargamax = Cargamax;
            this.matricula = MATRICULA;
        }

        //2 letras 3 numeros 2 letras
        public override bool Matricula(string validar)
        {
            if (validar != null)
            {
                string validadorMatriculCamion = @"^[a-zA-Z]{2}\d{3}[a-zA-Z]{2}$";
                bool validarMatricula = Regex.IsMatch(validar, validadorMatriculCamion);
                return validarMatricula;
            }
            return false;
        }

        public override string Mostrar()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"TORQUE :{this.torque}");
            sb.AppendLine($"CARGA MAXIMA : {this.cargamax}");
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
                    torque == other.torque &&
                    cargamax == other.cargamax &&
                    matricula == other.matricula;
        }

        public bool Equals(Camion other)
        {
            return !(other is null) &&
                   base.Equals(other) &&
                   modelo == other.modelo &&
                   ano == other.ano &&
                   kilometraje == other.kilometraje &&
                   motor == other.motor &&
                   precio == other.precio &&
                   torque == other.torque &&
                   cargamax == other.cargamax &&
                   matricula == other.matricula;
        }

        public override int GetHashCode()
        {
            int hashCode = 716249854;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(modelo);
            hashCode = hashCode * -1521134295 + ano.GetHashCode();
            hashCode = hashCode * -1521134295 + kilometraje.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(motor);
            hashCode = hashCode * -1521134295 + precio.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<float>.Default.GetHashCode(torque);
            hashCode = hashCode * -1521134295 + EqualityComparer<float>.Default.GetHashCode(cargamax);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(matricula);
            return hashCode;
        }

        public static bool operator ==(Camion left, Camion right)
        {
            return EqualityComparer<Camion>.Default.Equals(left, right);
        }

        public static bool operator !=(Camion left, Camion right)
        {
            return !(left == right);
        }
    }
}
