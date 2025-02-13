using Aplicasion_WF_Consecionaria.CarpetaFrmVehiculo;
using Biblioteca_de_clases_concesionaria;
using Biblioteca_de_clases_concesionaria.Cvehiculos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Biblioteca_de_clases_concesionaria.Persona;


namespace Aplicasion_WF_Consecionaria
{
    public partial class frmAdmVehiculos : Form
    {
        private bool banderaCerrar;

        public string Matricula;

        public bool bandera;

        public bool acendente_decendente = true;

        private readonly string projectDirectory;
        private readonly string absolutePath;

        private Administrador administrador;

        private DaoVehiculosSql DaoVehiculosSql;

        private string Puesto;
        public frmAdmVehiculos(string Puesto)
        {
            InitializeComponent();

            projectDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string relativePath = Path.Combine(projectDirectory, @"..\..\archivosjs\");
            absolutePath = Path.GetFullPath(relativePath);

            this.DaoVehiculosSql = new DaoVehiculosSql();
            this.administrador = new Administrador();

            InitializeDataGridView();

            this.Load += new EventHandler(admEmpleados_Load);

            bandera = false;
            cmbVehiculos.DataSource = Enum.GetValues(typeof(Vehiculo.Tipovehiculo));
            cmbVehiculos.SelectedIndexChanged += cmbVehiculos_SelectedIndexChanged;

            comboBox1.Items.Add("precio");
            comboBox1.Items.Add("matricula");
            comboBox1.SelectedItem = "precio";
            
            this.Puesto = Puesto;
            OcultarInterfacess();
            CargarDatos();

            this.FormClosing += new FormClosingEventHandler(this.FrmAdministrador_FormClosing);
        }

        private void FrmAdministrador_FormClosing(object sender, FormClosingEventArgs e)
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


        private void OcultarInterfacess()
        {
            if (this.Puesto == "Cliente")
            {
                btnAgregar.Visible = false;
                btnEliminar.Visible = false;
                btnModificar.Visible = false;
            }
            else if (this.Puesto == "Empleado")
            {
                btnEliminar.Visible = false;
            }
            else if (this.Puesto == "Administrador")
            {

            }

        }
        private void InitializeDataGridView()
        {
            // Configura propiedades del DataGridView
            dgvVehiculos.AutoGenerateColumns = true;
            dgvVehiculos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvVehiculos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void admEmpleados_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void btn_acendente_decendente_Click(object sender, EventArgs e)
        {
            if (acendente_decendente == false)
            {
                acendente_decendente = true;
                btn_acendente_decendente.Text = "Ascendente";
            }
            else
            {
                acendente_decendente = false;
                btn_acendente_decendente.Text = "Descendente";
            }
            CargarDatos();
        }

        private string tipodevalor()
        {
            if (comboBox1.Text == "precio")
            {
                return "precio";
            }
            else
            {
                return "matricula";
            }
        }


        private void MostrarDatos<T>(List<T> listaVehiculos)
        {
            try
            {
                if (listaVehiculos == null || listaVehiculos.Count == 0)
                {
                    dgvVehiculos.DataSource = null; // Limpiar la fuente de datos anterior para evitar problemas
                }

                dgvVehiculos.DataSource = null; // Limpiar la fuente de datos anterior para evitar problemas
                dgvVehiculos.DataSource = listaVehiculos;
                dgvVehiculos.SelectionChanged += new EventHandler(DataGridView1_SelectionChanged);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Se produjo un error al mostrar los datos: {ex.Message}\n", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void CargarDatos()
        {
            string selectedVehicleType = cmbVehiculos.SelectedItem.ToString();
            Vehiculos_central vehiculos_Central = new Vehiculos_central();

            if (selectedVehicleType == Vehiculo.Tipovehiculo.Auto.ToString())
            {
                List<Auto> autosOrdenados = this.administrador.LeerAuto(tipodevalor(), acendente_decendente);
                MostrarDatos<Auto>(autosOrdenados);
            }
            else if (selectedVehicleType == Vehiculo.Tipovehiculo.Moto.ToString())
            {
                List<Moto> motosOrdenados = this.administrador.LeerMoto(tipodevalor(), acendente_decendente);
                MostrarDatos<Moto>(motosOrdenados);
            }
            else if (selectedVehicleType == Vehiculo.Tipovehiculo.Camion.ToString())
            {
                List<Camion> camionesOrdenados = this.administrador.LeerCamion(tipodevalor(), acendente_decendente);
                MostrarDatos<Camion>(camionesOrdenados);
            }
            else if (selectedVehicleType == Vehiculo.Tipovehiculo.Deportivo.ToString())
            {
                List<Deportivo> deportivosOrdenados = this.administrador.LeerDeportivo(tipodevalor(), acendente_decendente);
                MostrarDatos<Deportivo>(deportivosOrdenados);
            }
        }


        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvVehiculos.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvVehiculos.SelectedRows[0];
                string selectedVehicleType = cmbVehiculos.SelectedItem.ToString();
                    
                if (selectedVehicleType == Vehiculo.Tipovehiculo.Auto.ToString())
                {
                    Auto selectedAuto = selectedRow.DataBoundItem as Auto;
                    if (selectedAuto != null)
                    {
                        Matricula = selectedAuto.matricula;
                    }
                }
                else if (selectedVehicleType == Vehiculo.Tipovehiculo.Moto.ToString())
                {
                    Moto selectedMoto = selectedRow.DataBoundItem as Moto;
                    if (selectedMoto != null)
                    {
                        Matricula = selectedMoto.matricula;
                    }
                }
                else if (selectedVehicleType == Vehiculo.Tipovehiculo.Camion.ToString())
                {
                    Camion selectedCamion = selectedRow.DataBoundItem as Camion;
                    if (selectedCamion != null)
                    {
                        Matricula = selectedCamion.matricula;
                    }
                }
                else if (selectedVehicleType == Vehiculo.Tipovehiculo.Deportivo.ToString())
                {
                    Deportivo selectedDeportivo = selectedRow.DataBoundItem as Deportivo;
                    if (selectedDeportivo != null)
                    {
                        Matricula = selectedDeportivo.matricula;
                    }
                }
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            bandera = false;
            string selectedVehicleType = cmbVehiculos.SelectedItem.ToString();

            FrmVehiculos secondaryForm = new FrmVehiculos(Matricula, bandera, selectedVehicleType);
            DialogResult result = secondaryForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                CargarDatos();
            }
            else if (result == DialogResult.Cancel)
            {
                CargarDatos();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string selectedVehicleType = cmbVehiculos.SelectedItem.ToString();

            if (selectedVehicleType == Vehiculo.Tipovehiculo.Deportivo.ToString())
            {
                if (!string.IsNullOrEmpty(Matricula))
                {
                    this.administrador.EliminarVehiculo(Matricula, Vehiculo.Tipovehiculo.Deportivo);
                    CargarDatos();
                }
                else
                {
                    MessageBox.Show("Seleccione un deportivo para eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            if (selectedVehicleType == Vehiculo.Tipovehiculo.Moto.ToString())
            {
                if (!string.IsNullOrEmpty(Matricula))
                {
                    this.administrador.EliminarVehiculo(Matricula, Vehiculo.Tipovehiculo.Moto);
                    CargarDatos();
                }
                else
                {
                    MessageBox.Show("Seleccione una Moto para eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            if (selectedVehicleType == Vehiculo.Tipovehiculo.Camion.ToString())
            {
                if (!string.IsNullOrEmpty(Matricula))
                {
                    this.administrador.EliminarVehiculo(Matricula, Vehiculo.Tipovehiculo.Camion);
                    CargarDatos();
                }
                else
                {
                    MessageBox.Show("Seleccione un auto para eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            if (selectedVehicleType == Vehiculo.Tipovehiculo.Auto.ToString())
            {
                if (!string.IsNullOrEmpty(Matricula))
                {
                    this.administrador.EliminarVehiculo(Matricula, Vehiculo.Tipovehiculo.Auto);
                    CargarDatos();
                }
                else
                {
                    MessageBox.Show("Seleccione un auto para eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            bandera = true;
            string selectedVehicleType = cmbVehiculos.SelectedItem.ToString();
            if (!string.IsNullOrEmpty(Matricula))
            {
                FrmVehiculos secondaryForm = new FrmVehiculos(Matricula, bandera, selectedVehicleType);
                DialogResult result = secondaryForm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    CargarDatos();

                }
                else if (result == DialogResult.Cancel)
                {
                    CargarDatos();

                }
            }
            else
            {
                MessageBox.Show($"Seleccione algun {selectedVehicleType}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            banderaCerrar = true;
            Application.OpenForms["frmMenu"].Show();
            this.Close();

        }

        private void cmbVehiculos_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void dgvVehiculos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void frmAdministrador_Load(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
