using Biblioteca_de_clases_concesionaria;
using Biblioteca_de_clases_concesionaria.Cvehiculos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Aplicasion_WF_Consecionaria.CarpetaFrmVehiculo
{
    public partial class FrmVehiculos : Form
    {

        private string Matricula;
        private bool bandera;
        private bool banderaCerrar;
        private string Tipo_Vehiculo;


        public FrmVehiculos(string matricula, bool bandera, string Tipo_Vehiculo)
        {
            this.Tipo_Vehiculo = Tipo_Vehiculo;
            InitializeComponent();
            this.Matricula = matricula;
            this.bandera = bandera;
            Label();
            CargarDatos();
            this.FormClosing += new FormClosingEventHandler(this.FrmAuto_FormClosing);
        }


        private void Label()
        {
            if (Tipo_Vehiculo == "Auto")
            {
                lblGeneral.Text = "cantidad de puertas";
                lblGerenal1.Text = "cantidad de pasajeros";
            }
            else if (Tipo_Vehiculo == "Deportivo")
            {
                lblGeneral.Text = "cantidad de puertas";
                lblGerenal1.Text = "cantidad de pasajeros";
            }
            else if (Tipo_Vehiculo == "Camion")
            {
                lblGeneral.Text = "Torque";
                lblGerenal1.Text = "Carga Maxima";
            }
            else if (Tipo_Vehiculo == "Moto")
            {
                lblGeneral.Text = "cilindrada";
                lblGerenal1.Text = "peso";
            }
        }


        private void FrmAuto_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (banderaCerrar)
            {
                e.Cancel = false;
            }
            else
            {
                FrmCerrar secondaryForm = new FrmCerrar();
                DialogResult result = secondaryForm.ShowDialog();
                if (result != DialogResult.OK)
                {
                    e.Cancel = true;
                }
            }
        }

        private void Agregar_vehiculo(Vehiculo vehiculo)
        {
            Administrador administrador = new Administrador();
            DaoVehiculosSql daoVehiculosSql = new DaoVehiculosSql();
            bool resultado = daoVehiculosSql == vehiculo;
            if (resultado == true && bandera == false)
            {
                MessageBox.Show("Esta matricula ya esta con otro vehiculo verifique que sea la correcta");
            }
            else
            {
                if (bandera == true)
                {
                    MessageBox.Show(administrador.ModificarVehiculo(this.Matricula, vehiculo,txtMatricula.Text));
                }
                else if (bandera == false)
                {
                    administrador.AgregarVeiculo(vehiculo);
                    MessageBox.Show($"{Tipo_Vehiculo} agregado exitosamente.");
                }
            }
        }

        private bool validarDatos()
        {
            if (string.IsNullOrWhiteSpace(txtModelo.Text) || string.IsNullOrWhiteSpace(txtAno.Text) || string.IsNullOrWhiteSpace(txtKilometros.Text) || string.IsNullOrWhiteSpace(txtMotor.Text) || string.IsNullOrWhiteSpace(txtPrecio.Text) || string.IsNullOrWhiteSpace(txtMatricula.Text) || string.IsNullOrWhiteSpace(txtGeneral.Text) || string.IsNullOrWhiteSpace(txtGeneral1.Text)) 
            {
                MessageBox.Show("Por favor, ingrese un valor en todas las casillas", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if(txtMatricula.Text != null )
            {
                return ValidarMatriculas();
            }
            return true;
        }

        private bool ValidarMatriculas()
        {
            bool validarMatriculasBandera = false;
            Administrador administrador = new Administrador();
            validarMatriculasBandera =  administrador.ValidarMatriculas(Tipo_Vehiculo, txtMatricula.Text);

            if (!validarMatriculasBandera)
            {
                MessageBox.Show("Por favor, ingrese un valor de matricula correcto");
            }
            return validarMatriculasBandera;
        }

        

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (validarDatos())
            {
                string modelo = txtModelo.Text;
                int ano = int.Parse(txtAno.Text);
                int kilometros = int.Parse(txtKilometros.Text);
                string motor = txtMotor.Text;
                int precio = int.Parse(txtPrecio.Text);

                if (Tipo_Vehiculo == "Auto")
                {
                    int puertas = int.Parse(txtGeneral.Text);
                    int pasajeros = int.Parse(txtGeneral1.Text);
                    string matricula = txtMatricula.Text;
                        
                    Auto auto = new Auto(modelo, ano, kilometros, motor, precio, puertas, pasajeros, matricula);
                    Agregar_vehiculo(auto);
                        
                }
                else if (Tipo_Vehiculo == "Deportivo")
                {
                    int puertas = int.Parse(txtGeneral.Text);
                    int pasajeros = int.Parse(txtGeneral1.Text);
                    string matricula = txtMatricula.Text;
                    Deportivo deportivo = new Deportivo(modelo, ano, kilometros, motor, precio, puertas, pasajeros, matricula);
                    Agregar_vehiculo(deportivo);
                }
                else if (Tipo_Vehiculo == "Camion")
                {
                    float torque = float.Parse(txtGeneral.Text);
                    float cargamax = float.Parse(txtGeneral1.Text);
                    string matricula = txtMatricula.Text;
                    Camion camion = new Camion(modelo, ano, kilometros, motor, precio, torque, cargamax, matricula);
                   
                    Agregar_vehiculo(camion);
                }
                else if (Tipo_Vehiculo == "Moto")
                {
                    int cilindrada = int.Parse(txtGeneral.Text);
                    int peso = int.Parse(txtGeneral1.Text);
                    string matricula = txtMatricula.Text;
                    Moto moto = new Moto(modelo, ano, kilometros, motor, precio, cilindrada, peso, matricula);
                    Agregar_vehiculo(moto);
                }
                
                banderaCerrar = true;
                LimpiarTxtBox();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            
        }

        private void LimpiarTxtBox()
        {
            txtModelo.Text = "";
            txtAno.Text = "";
            txtGeneral1.Text = "";
            txtMotor.Text = "";
            txtPrecio.Text = "";
            txtGeneral.Text = "";
            txtGeneral1.Text = "";
            txtMatricula.Text = "";
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            banderaCerrar = true;
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void CargarDatos()
        {
            if (bandera)
            {
                DaoVehiculosSql daoVehiculosSql = new DaoVehiculosSql();

                if (Tipo_Vehiculo == "Auto")
                {
                    List<Auto> listaAutos = daoVehiculosSql.LeerAuto();
                    Auto auto = listaAutos.FirstOrDefault(a => a.matricula == this.Matricula);
                    if (auto != null)
                    {
                        txtModelo.Text = auto.modelo;
                        txtAno.Text = auto.ano.ToString();
                        txtKilometros.Text = auto.kilometraje.ToString();
                        txtMotor.Text = auto.motor;
                        txtPrecio.Text = auto.precio.ToString();
                        txtMatricula.Text = auto.matricula;
                        txtGeneral.Text = auto.puertas.ToString();
                        txtGeneral1.Text = auto.cantidadDePasajeros.ToString();
                    }
                }
                if (Tipo_Vehiculo == "Camion")
                {
                    List<Camion> listaCamions = daoVehiculosSql.LeerCamiones();
                    Camion camion = listaCamions.FirstOrDefault(a => a.matricula == this.Matricula);
                    if (camion != null)
                    {
                        txtModelo.Text = camion.modelo;
                        txtAno.Text = camion.ano.ToString();
                        txtKilometros.Text = camion.kilometraje.ToString();
                        txtMotor.Text = camion.motor;
                        txtPrecio.Text = camion.precio.ToString();
                        txtMatricula.Text = camion.matricula;
                        txtGeneral.Text = camion.torque.ToString();
                        txtGeneral1.Text = camion.cargamax.ToString();
                    }
                }
                if (Tipo_Vehiculo == "Moto")
                {
                    List<Moto> listaMotos = daoVehiculosSql.LeerMotos();
                    Moto moto = listaMotos.FirstOrDefault(a => a.matricula == this.Matricula);
                    if (moto != null)
                    {
                        txtModelo.Text = moto.modelo;
                        txtAno.Text = moto.ano.ToString();
                        txtKilometros.Text = moto.kilometraje.ToString();
                        txtMotor.Text = moto.motor;
                        txtPrecio.Text = moto.precio.ToString();
                        txtMatricula.Text = moto.matricula;
                        txtGeneral.Text = moto.cilindrada.ToString();
                        txtGeneral1.Text = moto.peso.ToString();
                    }
                }
                if (Tipo_Vehiculo == "Deportivo")
                {
                    List<Deportivo> listaDeportivos = daoVehiculosSql.LeerDeportivos();
                    Deportivo deportivo = listaDeportivos.FirstOrDefault(a => a.matricula == this.Matricula);
                    if (deportivo != null)
                    {
                        txtModelo.Text = deportivo.modelo;
                        txtAno.Text = deportivo.ano.ToString();
                        txtKilometros.Text = deportivo.kilometraje.ToString();
                        txtMotor.Text = deportivo.motor;
                        txtPrecio.Text = deportivo.precio.ToString();
                        txtMatricula.Text = deportivo.matricula;
                        txtGeneral.Text = deportivo.puertas.ToString();
                        txtGeneral1.Text = deportivo.cantidadDePasajeros.ToString();
                    }
                }
            }
        }
    }
}
