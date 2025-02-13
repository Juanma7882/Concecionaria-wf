using Aplicasion_WF_Consecionaria.CarpetaFrmVehiculo;
using Biblioteca_de_clases_concesionaria;
using Aplicasion_WF_Consecionaria;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Biblioteca_de_clases_concesionaria.Persona;
using Biblioteca_de_clases_concesionaria.Carpeta_personas;

namespace Aplicasion_WF_Consecionaria
{
    public partial class frmMenu : Form
    {
        private int Dni;
        private string NombreCompleto;
        private string AbsolutePath;
        private bool banderaCerrar;
        private string Puesto;


        public frmMenu(int dni,string absolutePath,string Puesto)
        {
            InitializeComponent();
            this.Puesto = Puesto;
            this.Load += new EventHandler(frmAdministrador_Load);
            this.Dni = dni;
            this.AbsolutePath = absolutePath;
            this.FormClosing += new FormClosingEventHandler(this.FrmMenu_FormClosing);
            visibilidadDeLosComponentes();


            DaoVehiculosSql sql = new DaoVehiculosSql();
            sql.preInicializadorDaoSql += PreinicializarSql; // Suscripción al evento
            sql.PreInicializadorDaoSql();
        }

        private void visibilidadDeLosComponentes()
        {
            if (this.Puesto == "Administrador" )
            {
                btnPerfil.Visible = true;
            }
            else
            {
                btnPerfil.Visible = false;
                btnAdmEmpleados.Visible = false;
            }

        }


        private void FrmMenu_FormClosing(object sender, FormClosingEventArgs e)
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


        private void Buscar<T>(List<T>  clienteleidos) where T : Persona
        {
            foreach (var cliente in clienteleidos)
            {
                if (cliente.Dni == this.Dni)
                {
                    this.NombreCompleto = $"{cliente.Nombre} {cliente.Apellido} {Puesto}";
                }
            }
        }
        
        private void Persona()
        {
            Personas_central personas_Central = new Personas_central();

            List<Cliente> clienteleidos = personas_Central.LeerCliente();
            List<Empleado> Empleadoleidos = personas_Central.LeerEmpleado();
            List<Administrador> admileidos =  personas_Central.LeerAdministrador();
            if (Puesto == "Cliente")
            {
                Buscar(clienteleidos);
            }
            else if (Puesto == "Empleado")
            {
                Buscar(Empleadoleidos);
            }
            else if (Puesto == "Administrador")
            {
                Buscar(admileidos);
            }
        }


        private void frmAdministrador_Load(object sender, EventArgs e)
        {
            DateTime fechaActual = DateTime.Now;
            labelFecha.Text = fechaActual.ToString("dd/MM/yyyy");
            Persona();
            labelNombreCompleto.Text = this.NombreCompleto;
            GuardarFechaActual();
        }

        private void GuardarFechaActual()
        {
            DateTime fechaActual = DateTime.Now;
            string time = fechaActual.ToString("dd/MM/yyyy hh:mm:ss tt");
            ManejadorLog manejadorLog = new ManejadorLog();
            manejadorLog.EscribirLog(time, this.NombreCompleto);
        }


        private void btnPerfil_Click(object sender, EventArgs e)
        {
            FrmPerfil menu = new FrmPerfil(this.Dni);
            this.Hide();
            menu.Show();
        }

        private void btnAdmVehiculos_Click(object sender, EventArgs e)
        {
            frmAdmVehiculos menu = new frmAdmVehiculos(this.Puesto);
            this.Hide();
            menu.Show();
        }


        private void btnAdmEmpleados_Click(object sender, EventArgs e)
        {
            frmAdmPersonas menu = new frmAdmPersonas();
            this.Hide();
            menu.Show();
        }

        private void btnCerrarSecion_Click(object sender, EventArgs e)
        {
            banderaCerrar = true;
            this.FormClosing += new FormClosingEventHandler(this.FrmMenu_FormClosing);

            Application.OpenForms["frmLogin"].Show();
            this.Close();
        }

        private void PreinicializarSql(object sender, EventArgs e) { }
    }
}
