namespace Aplicasion_WF_Consecionaria
{
    partial class frmMenu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnCerrarSecion = new System.Windows.Forms.Button();
            this.btnAdmEmpleados = new System.Windows.Forms.Button();
            this.btnAdmVehiculos = new System.Windows.Forms.Button();
            this.btnPerfil = new System.Windows.Forms.Button();
            this.labelFecha = new System.Windows.Forms.Label();
            this.labelNombreCompleto = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnCerrarSecion
            // 
            this.btnCerrarSecion.BackColor = System.Drawing.Color.Brown;
            this.btnCerrarSecion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrarSecion.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnCerrarSecion.Location = new System.Drawing.Point(2, 2);
            this.btnCerrarSecion.Name = "btnCerrarSecion";
            this.btnCerrarSecion.Size = new System.Drawing.Size(172, 45);
            this.btnCerrarSecion.TabIndex = 7;
            this.btnCerrarSecion.Text = "Cerrar sesion";
            this.btnCerrarSecion.UseVisualStyleBackColor = false;
            this.btnCerrarSecion.Click += new System.EventHandler(this.btnCerrarSecion_Click);
            // 
            // btnAdmEmpleados
            // 
            this.btnAdmEmpleados.Image = global::Aplicasion_WF_Consecionaria.Properties.Resources.icons8_grupo_de_usuario_60;
            this.btnAdmEmpleados.Location = new System.Drawing.Point(561, 144);
            this.btnAdmEmpleados.Name = "btnAdmEmpleados";
            this.btnAdmEmpleados.Size = new System.Drawing.Size(183, 143);
            this.btnAdmEmpleados.TabIndex = 6;
            this.btnAdmEmpleados.Text = "Administrar Empleados";
            this.btnAdmEmpleados.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAdmEmpleados.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAdmEmpleados.UseVisualStyleBackColor = true;
            this.btnAdmEmpleados.Click += new System.EventHandler(this.btnAdmEmpleados_Click);
            // 
            // btnAdmVehiculos
            // 
            this.btnAdmVehiculos.Image = global::Aplicasion_WF_Consecionaria.Properties.Resources.icons8_auto_50;
            this.btnAdmVehiculos.Location = new System.Drawing.Point(332, 144);
            this.btnAdmVehiculos.Name = "btnAdmVehiculos";
            this.btnAdmVehiculos.Size = new System.Drawing.Size(185, 143);
            this.btnAdmVehiculos.TabIndex = 5;
            this.btnAdmVehiculos.Text = "Administrar Vehiculos";
            this.btnAdmVehiculos.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAdmVehiculos.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAdmVehiculos.UseVisualStyleBackColor = true;
            this.btnAdmVehiculos.Click += new System.EventHandler(this.btnAdmVehiculos_Click);
            // 
            // btnPerfil
            // 
            this.btnPerfil.Image = global::Aplicasion_WF_Consecionaria.Properties.Resources.icons8_usuario_60;
            this.btnPerfil.Location = new System.Drawing.Point(92, 144);
            this.btnPerfil.Name = "btnPerfil";
            this.btnPerfil.Size = new System.Drawing.Size(185, 143);
            this.btnPerfil.TabIndex = 4;
            this.btnPerfil.Text = "Perfil";
            this.btnPerfil.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnPerfil.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnPerfil.UseVisualStyleBackColor = true;
            this.btnPerfil.Click += new System.EventHandler(this.btnPerfil_Click);
            // 
            // labelFecha
            // 
            this.labelFecha.AutoSize = true;
            this.labelFecha.Location = new System.Drawing.Point(13, 418);
            this.labelFecha.Name = "labelFecha";
            this.labelFecha.Size = new System.Drawing.Size(0, 20);
            this.labelFecha.TabIndex = 8;
            // 
            // labelNombreCompleto
            // 
            this.labelNombreCompleto.AutoSize = true;
            this.labelNombreCompleto.Location = new System.Drawing.Point(565, 418);
            this.labelNombreCompleto.Name = "labelNombreCompleto";
            this.labelNombreCompleto.Size = new System.Drawing.Size(0, 20);
            this.labelNombreCompleto.TabIndex = 9;
            // 
            // frmMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 463);
            this.Controls.Add(this.labelNombreCompleto);
            this.Controls.Add(this.labelFecha);
            this.Controls.Add(this.btnCerrarSecion);
            this.Controls.Add(this.btnAdmEmpleados);
            this.Controls.Add(this.btnAdmVehiculos);
            this.Controls.Add(this.btnPerfil);
            this.Name = "frmMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmMenu";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCerrarSecion;
        private System.Windows.Forms.Button btnAdmEmpleados;
        private System.Windows.Forms.Button btnAdmVehiculos;
        private System.Windows.Forms.Button btnPerfil;
        private System.Windows.Forms.Label labelFecha;
        private System.Windows.Forms.Label labelNombreCompleto;
    }
}