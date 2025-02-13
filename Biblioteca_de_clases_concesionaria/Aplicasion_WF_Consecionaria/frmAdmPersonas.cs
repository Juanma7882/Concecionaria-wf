using Aplicasion_WF_Consecionaria.CarpetaFrmVehiculo;
using Biblioteca_de_clases_concesionaria.Cvehiculos;
using Biblioteca_de_clases_concesionaria;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Biblioteca_de_clases_concesionaria.Carpeta_personas;

namespace Aplicasion_WF_Consecionaria
{
    public partial class frmAdmPersonas : Form
    {
        private bool banderaCerrar;

        public int Dni;

        public bool bandera;

        public bool acendente_decendente = true;

        private readonly string projectDirectory;
        private readonly string absolutePath;

        private Personas_central personas_Central;

        private string selectedPersonaType;

        public frmAdmPersonas()
        {
            InitializeComponent();

            projectDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string relativePath = Path.Combine(projectDirectory, @"..\..\archivosjs\");
            absolutePath = Path.GetFullPath(relativePath);

            this.personas_Central = new Personas_central();

            InitializeDataGridView();

            this.Load += new EventHandler(admEmpleados_Load);
            bandera = false;

            cmbPersonas.DataSource = Enum.GetValues(typeof(Persona.TipoPersona));
            cmbPersonas.SelectedIndexChanged += cmbPersonas_SelectedIndexChanged;

            comboBox1.Items.Add("Nombre");
            comboBox1.Items.Add("Dni");
            comboBox1.SelectedItem = "Nombre";


            this.FormClosing += new FormClosingEventHandler(this.FrmAdministrador_FormClosing);

            selectedPersonaType = cmbPersonas.SelectedItem.ToString();

            CargarDatos();
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


        private void InitializeDataGridView()
        {
            // Configura propiedades del DataGridView
            dgvPersonas.AutoGenerateColumns = true;
            dgvPersonas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvPersonas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void admEmpleados_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void btn_acendente_decendente_Click_1(object sender, EventArgs e)
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
            if (comboBox1.Text == "Nombre")
            {
                return "Nombre";
            }
            else
            {
                return "Dni";
            }
        }


        private void MostrarDatos<T>(List<T> listaPersonas)
        {
            try
            {
                if (listaPersonas == null || listaPersonas.Count == 0)
                {
                    dgvPersonas.DataSource = null; // Limpiar la fuente de datos anterior para evitar problemas
                }
                dgvPersonas.DataSource = null; // Limpiar la fuente de datos anterior para evitar problemas
                dgvPersonas.DataSource = listaPersonas;
                dgvPersonas.SelectionChanged += new EventHandler(DataGridView1_SelectionChanged);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Se produjo un error al mostrar los datos: {ex.Message}\n", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarDatos()
        {

            if (selectedPersonaType == Persona.TipoPersona.Administrador.ToString())
            {
                List<Administrador> listaAdministradors = this.personas_Central.LeerAdministrador();
                List<Administrador> admOrdenados = this.personas_Central.Ordenar_por_dato(tipodevalor(), listaAdministradors, acendente_decendente);
                MostrarDatos<Administrador>(admOrdenados);
            }
            else if (selectedPersonaType == Persona.TipoPersona.Empleado.ToString())
            {
                List<Empleado> listaEmpleados = this.personas_Central.LeerEmpleado();
                List<Empleado> empOrdenados = this.personas_Central.Ordenar_por_dato(tipodevalor(), listaEmpleados, acendente_decendente);
                MostrarDatos<Empleado>(empOrdenados);
            }
            else if (selectedPersonaType == Persona.TipoPersona.Cliente.ToString())
            {
                List<Cliente> listaClientees = this.personas_Central.LeerCliente();
                List<Cliente> clienOrdenados = this.personas_Central.Ordenar_por_dato(tipodevalor(), listaClientees, acendente_decendente);
                MostrarDatos<Cliente>(clienOrdenados);
            }
        }


        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPersonas.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvPersonas.SelectedRows[0];

                if (selectedPersonaType == Persona.TipoPersona.Administrador.ToString())
                {
                    Administrador selectedAdministrador = selectedRow.DataBoundItem as Administrador;
                    if (selectedAdministrador != null)
                    {
                        Dni = selectedAdministrador.Dni;
                    }
                }
                else if (selectedPersonaType == Persona.TipoPersona.Empleado.ToString())
                {
                    Empleado selectedEmpleado = selectedRow.DataBoundItem as Empleado;
                    if (selectedEmpleado != null)
                    {
                        Dni = selectedEmpleado.Dni;
                    }
                }
                else if (selectedPersonaType == Persona.TipoPersona.Cliente.ToString())
                {
                    Cliente selectedCliente = selectedRow.DataBoundItem as Cliente;
                    if (selectedCliente != null)
                    {
                        Dni = selectedCliente.Dni;
                    }
                }
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            bandera = false;

            FrmPersonas secondaryForm = new FrmPersonas(this.Dni, bandera, selectedPersonaType);
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

        private async void btnEliminar_Click_1(object sender, EventArgs e)
        {
            try
            {
                // Ejecutar la operación en un hilo separado
                await Task.Run(() =>
                {
                    string texDni = this.Dni.ToString();

                    if (selectedPersonaType == Persona.TipoPersona.Administrador.ToString())
                    {
                        if (this.Dni > 0)
                        {
                            this.personas_Central -= this.Dni;
                            // Invocar la actualización 
                            this.Invoke((MethodInvoker)delegate
                            {
                                CargarDatos();
                            });
                        }
                        else
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                MessageBox.Show($"Seleccione algun {selectedPersonaType}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            });
                        }
                    }
                    else if (selectedPersonaType == Persona.TipoPersona.Empleado.ToString())
                    {
                        if (this.Dni > 0)
                        {
                            this.personas_Central -= this.Dni;
                            this.Invoke((MethodInvoker)delegate
                            {
                                CargarDatos();
                            });
                        }
                        else
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                MessageBox.Show($"Seleccione algun {selectedPersonaType}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            });
                        }
                    }
                    else if (selectedPersonaType == Persona.TipoPersona.Cliente.ToString())
                    {
                        if (this.Dni > 0)
                        {
                            this.personas_Central -= this.Dni;
                            this.Invoke((MethodInvoker)delegate
                            {
                                CargarDatos();
                            });
                        }
                        else
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                MessageBox.Show($"Seleccione algun {selectedPersonaType}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            });
                        }
                    }
                });

                // Actualización adicional 
                this.Invoke((MethodInvoker)delegate
                {
                    MessageBox.Show("Operación completada", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                });
            }
            catch (Exception ex)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    MessageBox.Show($"Ocurrió un error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                });
            }
            CargarDatos();

        }

        private void btnModificar_Click_1(object sender, EventArgs e)
        {
            bandera = true;
            if (this.Dni > 0)
            {
                MessageBox.Show(Dni.ToString());
                FrmPersonas secondaryForm = new FrmPersonas(Dni, bandera, selectedPersonaType);
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
                MessageBox.Show($"Seleccione algun {selectedPersonaType}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void cmbPersonas_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedPersonaType = cmbPersonas.SelectedItem.ToString();
            CargarDatos();
        }

        private void dgvPersonas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void frmAdministrador_Load(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void btnRegresar_Click_1(object sender, EventArgs e)
        {
            banderaCerrar = true;
            Application.OpenForms["frmMenu"].Show();
            this.Close();
        }

        private void dgvPersonas_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
