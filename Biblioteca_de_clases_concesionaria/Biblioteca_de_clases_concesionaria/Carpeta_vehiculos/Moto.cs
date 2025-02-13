using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Biblioteca_de_clases_concesionaria.Cvehiculos
{
    public class Moto : Vehiculo, IEquatable<Moto>
    {
        public int cilindrada { get; set; }
        public int peso { get; set; }
        public string matricula { get; set; }

        public Moto()
        {
        }

        private Moto(string MODELO, int ANO, int KILOMETRAJE, string MOTOR, int PRECIO)
            : base(MODELO, ANO, KILOMETRAJE, MOTOR, PRECIO)
        {
        }

        public Moto(string MODELO, int ANO, int KILOMETRAJE, string MOTOR, int PRECIO, int CILINDRADA, int PESO, string MATRICULA)
        : base(MODELO, ANO, KILOMETRAJE, MOTOR, PRECIO)
        {
            this.precio = PRECIO;
            this.peso = PESO;
            this.matricula = MATRICULA;
            this.cilindrada = CILINDRADA;
        }


        public override bool Matricula(string validar)
        {
            if (validar != null)
            {
                string validadorMatriculMoto = @"^[a-zA-Z]{4}\d{3}$";

                bool validarMatricula = Regex.IsMatch(validar, validadorMatriculMoto);
                return validarMatricula;
            }
            return false;
        }

        public override string Mostrar()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"CILINDRADA :{this.cilindrada}");
            sb.AppendLine($"PESO : {this.peso}");
            sb.AppendLine($"MATRICULA : {this.matricula}");
            return sb.ToString();
        }
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Moto other = (Moto)obj;
            return base.Equals(other) &&
                    cilindrada == other.cilindrada &&
                    peso == other.peso &&
                    matricula == other.matricula;
        }

        public bool Equals(Moto other)
        {
            return !(other is null) &&
                    base.Equals(other) &&
                    modelo == other.modelo &&
                    ano == other.ano &&
                    kilometraje == other.kilometraje &&
                    motor == other.motor &&
                    precio == other.precio &&
                    cilindrada == other.cilindrada &&
                    peso == other.peso &&
                    matricula == other.matricula;
        }

        public override int GetHashCode()
        {
            int hashCode = -1684914916;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(modelo);
            hashCode = hashCode * -1521134295 + ano.GetHashCode();
            hashCode = hashCode * -1521134295 + kilometraje.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(motor);
            hashCode = hashCode * -1521134295 + precio.GetHashCode();
            hashCode = hashCode * -1521134295 + cilindrada.GetHashCode();
            hashCode = hashCode * -1521134295 + peso.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(matricula);
            return hashCode;
        }

        public static bool operator ==(Moto left, Moto right)
        {
            return EqualityComparer<Moto>.Default.Equals(left, right);
        }

        public static bool operator !=(Moto left, Moto right)
        {
            return !(left == right);
        }


    }
}
