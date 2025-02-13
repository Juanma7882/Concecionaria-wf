using Biblioteca_de_clases_concesionaria.Cvehiculos;
using Biblioteca_de_clases_concesionaria;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Biblioteca_de_clases_concesionaria.Carpeta_personas;

namespace Aplicasion_WF_Consecionaria.CarpetaFrmVehiculo
{
    public partial class FrmPersonas : Form
    {
        private int Dni;
        private bool bandera;
        private bool banderaCerrar;
        private string Tipo_Persona;


        public FrmPersonas(int Dni, bool bandera, string Tipo_Persona)
        {
            this.Tipo_Persona = Tipo_Persona;
            InitializeComponent();
            this.Dni = Dni;
            this.bandera = bandera;
            CargarDatos();

            this.FormClosing += new FormClosingEventHandler(this.FrmAuto_FormClosing);

            if (Tipo_Persona == "Administrador")
            {
                Administrador admin = new Administrador("", "", 0, "", "");
                admin.MostrarDatos += Admin_MostrarDatos; // Suscripción al evento
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

        private void Agregar_Persona(Persona persona)
        {
            Personas_central central = new Personas_central();

            bool resultado = central == persona;
            if(resultado)
            {
                MessageBox.Show("Este DNI ya lo tiene otra persona por favor revise los datos a ingresar");
            }
            else
            {
                if (bandera == true)
                {
                    central -= this.Dni;
                    central += persona;
                    MessageBox.Show($"EL {Tipo_Persona} con el DNI : {this.Dni} fue modificado exitosamente");

                }
                else if (bandera == false)
                {
                    central += persona;
                    MessageBox.Show($"{Tipo_Persona} agregado exitosamente.");
                }
            }
            
        }

        private bool validarDatos()
        {
            if ( string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtApellido.Text) || string.IsNullOrWhiteSpace(txtDni.Text) || string.IsNullOrWhiteSpace(txtDni.Text) || string.IsNullOrWhiteSpace(txtUsuario.Text) || string.IsNullOrWhiteSpace(txtContrasena.Text))
            {
                MessageBox.Show("Por favor, ingrese un valor en todas las casillas", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }


        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (validarDatos())
            {
                string nombre = txtNombre.Text;
                string apellido = txtApellido.Text;
                int dni = int.Parse(txtDni.Text);
                string usuario = txtUsuario.Text;
                string contrasena = txtContrasena.Text;

                if (Tipo_Persona == "Administrador")
                {
                   
                    Administrador admin = new Administrador(nombre, apellido, dni, usuario, contrasena);
                    Agregar_Persona(admin);
                    MessageBox.Show(admin.OnMostrar());

                }
                else if (Tipo_Persona == "Empleado")
                {
                   
                    Empleado Empleado = new Empleado(nombre, apellido, dni, usuario, contrasena);
                    Agregar_Persona(Empleado);
                }
                else if (Tipo_Persona == "Cliente")
                {
                    Cliente cliente = new Cliente(nombre, apellido, dni, usuario, contrasena);
                    Agregar_Persona(cliente);
                }

                LimpiarTxtBox();
                banderaCerrar = true;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }

        }

        private void LimpiarTxtBox()
        {
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtDni.Text = "";
            txtUsuario.Text = "";
            txtContrasena.Text = "";
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
                Personas_central vehiculos_Central = new Personas_central();

                if (Tipo_Persona == "Administrador")
                {
                    List<Administrador> listaAdministradores = vehiculos_Central.LeerAdministrador();
                    Administrador administrador = listaAdministradores.FirstOrDefault(a => a.Dni == this.Dni);
                    if (administrador != null)
                    {
                        txtNombre.Text = administrador.Nombre;
                        txtApellido.Text = administrador.Apellido.ToString();
                        txtDni.Text = administrador.Dni.ToString();
                        txtUsuario.Text = administrador.Usuario;
                        txtContrasena.Text = administrador.Contrasena.ToString();
                    }
                }
                if (Tipo_Persona == "Empleado")
                {
                    List<Empleado> listaEmpleado = vehiculos_Central.LeerEmpleado();
                    Empleado empleado = listaEmpleado.FirstOrDefault(a => a.Dni == this.Dni);
                    if (empleado != null)
                    {
                        txtNombre.Text = empleado.Nombre;
                        txtApellido.Text = empleado.Apellido.ToString();
                        txtDni.Text = empleado.Dni.ToString();
                        txtUsuario.Text = empleado.Usuario;
                        txtContrasena.Text = empleado.Contrasena.ToString();
                    }
                }
                if (Tipo_Persona == "Cliente")
                {
                    List<Cliente> listaClientees = vehiculos_Central.LeerCliente();
                    Cliente administrador = listaClientees.FirstOrDefault(a => a.Dni == this.Dni);
                    if (administrador != null)
                    {
                        txtNombre.Text = administrador.Nombre;
                        txtApellido.Text = administrador.Apellido.ToString();
                        txtDni.Text = administrador.Dni.ToString();
                        txtUsuario.Text = administrador.Usuario;
                        txtContrasena.Text = administrador.Contrasena.ToString();
                    }
                }
               
            }
        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            banderaCerrar = true;
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void Admin_MostrarDatos(object sender, EventArgs e) { }




    }
}
