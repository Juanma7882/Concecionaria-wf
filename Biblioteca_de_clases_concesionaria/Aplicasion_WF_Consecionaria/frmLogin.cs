using Biblioteca_de_clases_concesionaria;
using Biblioteca_de_clases_concesionaria.Carpeta_personas;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aplicasion_WF_Consecionaria
{
    public partial class frmLogin : Form
    {
        private readonly string projectDirectory;
        private readonly string absolutePath;
        private int Dni;


        public frmLogin()
        {
            InitializeComponent();
            projectDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string relativePath = Path.Combine(projectDirectory, @"..\..\archivosjs\");
            absolutePath = Path.GetFullPath(relativePath);

            txtUsuario.Text = "Usuario";
            textContrasena.Text = "Contraseña";

            this.FormClosing += new FormClosingEventHandler(this.FrmLogin_FormClosing);






        }

        private void FrmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            FrmCerrar secondaryForm = new FrmCerrar();
            DialogResult result = secondaryForm.ShowDialog();
            if (result != DialogResult.OK)
            {
                e.Cancel = true;
            }
        }

        private void btnCerrarPrograma_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public bool TipoUsuario<T>(List<T> lista, string txtuser, string txtpass) where T : Persona
        {
            foreach (var item in lista)
            {
                if (txtuser == item.Usuario && txtpass == item.Contrasena)
                {
                    this.Dni = item.Dni;
                    return true;
                }
            }
            return false;
        }



        private void btnIniciar_Click(object sender, EventArgs e)
        {

            string txtuser = txtUsuario.Text;
            string txtpass = textContrasena.Text;

            if (string.IsNullOrWhiteSpace(txtuser) || string.IsNullOrWhiteSpace(txtpass))
            {
                MessageBox.Show("Por favor, ingrese usuario y contraseña.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Personas_central personas_Central = new Personas_central();
            List<Cliente> clienteLeido = personas_Central.LeerCliente();
            List<Empleado> empleadosLeido = personas_Central.LeerEmpleado();
            List<Administrador> admileidos = personas_Central.LeerAdministrador();

            if (TipoUsuario(clienteLeido, txtuser,txtpass))
            {
                frmMenu menu = new frmMenu(this.Dni, absolutePath,"Cliente");
                        menu.Show();
                        this.Hide();
            }
            else if (TipoUsuario(empleadosLeido, txtuser, txtpass))
            {
                frmMenu menu = new frmMenu(this.Dni, absolutePath,"Empleado");
                menu.Show();
                this.Hide();
            }
            else if (TipoUsuario(admileidos, txtuser, txtpass))
            {
                frmMenu menu = new frmMenu(this.Dni, absolutePath,"Administrador");
                menu.Show();
                this.Hide();
            }
            else 
            {
                MessageBox.Show("Usuario o contraseña incorrectos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
                
        }



        private void btnVer_Click(object sender, EventArgs e)
        {
            if (textContrasena.UseSystemPasswordChar)
            {
                textContrasena.UseSystemPasswordChar = false;
            }
            else
            {
                textContrasena.UseSystemPasswordChar = true;
            }
        }
    }
}
