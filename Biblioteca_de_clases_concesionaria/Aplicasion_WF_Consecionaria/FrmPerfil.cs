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

namespace Aplicasion_WF_Consecionaria
{
    public partial class FrmPerfil : Form
    {
        private int Dni;
        private bool banderaCerrar;

        public FrmPerfil(int Dni)
        {
            this.Dni = Dni;
            InitializeComponent();
            Mostrartxt();
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


        private void MostrarUsuario()
        {
            //Daojson daojson = new Daojson(AbsolutePath, "Administrador.json");
            //List<Administrador> admileidos = daojson.LeerJson<Administrador>();
            //foreach (var admi in admileidos)
            //{
            //    NombreCompleto = $"{admi.Nombre} {admi.Apellido}";
            //}
        }


        private void Mostrartxt()
        {
            ManejadorLog manejadorLog = new ManejadorLog();
            rtboxLog.Text = manejadorLog.LeerLog();
        }

        private void rtboxLog_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            banderaCerrar = true;
            Application.OpenForms["frmMenu"].Show();
            this.Close();
        }
    }
}
