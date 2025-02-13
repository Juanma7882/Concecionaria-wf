using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca_de_clases_concesionaria.Cvehiculos
{
    public abstract class Vehiculo : IEquatable<Vehiculo>
    {
        public enum Tipovehiculo
        {
            Auto,
            Moto,
            Camion,
            Deportivo,
        }

        public string modelo { get; set; }
        public int ano { get; set; }
        public int kilometraje { get; set; }
        public string motor { get; set; }
        public int precio { get; set; }

        public Vehiculo(){}

        public Vehiculo(string MODELO, int ANO, int KILOMETRAJE, string MOTOR, int PRECIO)
        {
            this.modelo = MODELO;
            this.ano = ANO;
            this.kilometraje = KILOMETRAJE;
            this.motor = MOTOR;
            this.precio = PRECIO;
        }

        public abstract bool Matricula(string validar);


        public virtual string Mostrar()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"MODELO : {this.modelo}");
            sb.AppendLine($"AÑO : {this.ano.ToString()}");
            sb.AppendLine($"Kilometraje : {this.kilometraje.ToString()}");
            sb.AppendLine($"MOTOR : {this.motor}");
            sb.AppendLine($"PRECIO :{this.precio.ToString()}");
            return sb.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Vehiculo other = (Vehiculo)obj;
            return modelo == other.modelo && ano == other.ano && kilometraje == other.kilometraje && motor == other.motor;
        }

        public bool Equals(Vehiculo other)
        {
            return !(other is null) &&
                    modelo == other.modelo &&
                    ano == other.ano &&
                    kilometraje == other.kilometraje &&
                    motor == other.motor;
        }

        public override int GetHashCode()
        {
            int hashCode = 1333299114;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(modelo);
            hashCode = hashCode * -1521134295 + ano.GetHashCode();
            hashCode = hashCode * -1521134295 + kilometraje.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(motor);
            return hashCode;
        }

        public static bool operator ==(Vehiculo left, Vehiculo right)
        {
            return EqualityComparer<Vehiculo>.Default.Equals(left, right);
        }

        public static bool operator !=(Vehiculo left, Vehiculo right)
        {
            return !(left == right);
        }
    }
}
