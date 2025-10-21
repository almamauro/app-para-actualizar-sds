 namespace trabajo_fina_de_algoritmo_y_estructura
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtDniBusqueda = new TextBox();
            txtApellidoBusqueda = new TextBox();
            lstResultados = new ListBox();
            btnModificar = new Button();
            btnEliminar = new Button();
            btnAgregar = new Button();
            lblNombre = new Label();
            txtNombre = new TextBox();
            lblTelefono = new Label();
            txtTelefono = new TextBox();
            btnCancelar = new Button();
            btnGuardar = new Button();
            label1 = new Label();
            label2 = new Label();
            SuspendLayout();
            // 
            // txtDniBusqueda
            // 
            txtDniBusqueda.Location = new Point(288, 88);
            txtDniBusqueda.Name = "txtDniBusqueda";
            txtDniBusqueda.Size = new Size(105, 23);
            txtDniBusqueda.TabIndex = 0;
            txtDniBusqueda.TextChanged += txtBusqueda_TextChanged;
            // 
            // txtApellidoBusqueda
            // 
            txtApellidoBusqueda.Location = new Point(288, 23);
            txtApellidoBusqueda.Name = "txtApellidoBusqueda";
            txtApellidoBusqueda.Size = new Size(105, 23);
            txtApellidoBusqueda.TabIndex = 1;
            txtApellidoBusqueda.TextChanged += txtBusqueda_TextChanged;
            // 
            // lstResultados
            // 
            lstResultados.FormattingEnabled = true;
            lstResultados.ItemHeight = 15;
            lstResultados.Location = new Point(51, 188);
            lstResultados.Name = "lstResultados";
            lstResultados.Size = new Size(271, 94);
            lstResultados.TabIndex = 2;
            lstResultados.SelectedIndexChanged += lstResultados_SelectedIndexChanged_1;
            // 
            // btnModificar
            // 
            btnModificar.Location = new Point(653, 22);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(102, 49);
            btnModificar.TabIndex = 3;
            btnModificar.Text = "Modificar";
            btnModificar.UseVisualStyleBackColor = true;
            btnModificar.Click += btnModificar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(653, 114);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(102, 49);
            btnEliminar.TabIndex = 4;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnAgregar
            // 
            btnAgregar.Location = new Point(653, 205);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(102, 49);
            btnAgregar.TabIndex = 5;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = true;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Location = new Point(6, 25);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(51, 15);
            lblNombre.TabIndex = 6;
            lblNombre.Text = "Nombre\t";
            lblNombre.Visible = false;
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(63, 22);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(100, 23);
            txtNombre.TabIndex = 7;
            txtNombre.Visible = false;
            // 
            // lblTelefono
            // 
            lblTelefono.AutoSize = true;
            lblTelefono.Location = new Point(6, 91);
            lblTelefono.Name = "lblTelefono";
            lblTelefono.Size = new Size(52, 15);
            lblTelefono.TabIndex = 8;
            lblTelefono.Text = "Teléfono";
            lblTelefono.Visible = false;
            // 
            // txtTelefono
            // 
            txtTelefono.Location = new Point(63, 88);
            txtTelefono.Name = "txtTelefono";
            txtTelefono.Size = new Size(100, 23);
            txtTelefono.TabIndex = 9;
            txtTelefono.Visible = false;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(493, 188);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(102, 49);
            btnCancelar.TabIndex = 10;
            btnCancelar.Text = "cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Visible = false;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(493, 52);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(102, 49);
            btnGuardar.TabIndex = 11;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Visible = false;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(229, 92);
            label1.Name = "label1";
            label1.Size = new Size(24, 15);
            label1.TabIndex = 12;
            label1.Text = "dni";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(219, 25);
            label2.Name = "label2";
            label2.Size = new Size(52, 15);
            label2.TabIndex = 13;
            label2.Text = "apellido ";
            label2.Click += label2_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnGuardar);
            Controls.Add(btnCancelar);
            Controls.Add(txtTelefono);
            Controls.Add(lblTelefono);
            Controls.Add(txtNombre);
            Controls.Add(lblNombre);
            Controls.Add(btnAgregar);
            Controls.Add(btnEliminar);
            Controls.Add(btnModificar);
            Controls.Add(lstResultados);
            Controls.Add(txtApellidoBusqueda);
            Controls.Add(txtDniBusqueda);
            Name = "Form1";
            Text = " ";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtDniBusqueda;
        private TextBox txtApellidoBusqueda;
        private ListBox lstResultados;
        private Button btnModificar;
        private Button btnEliminar;
        private Button btnAgregar;
        private Label lblNombre;
        private TextBox txtNombre;
        private Label lblTelefono;
        private TextBox txtTelefono;
        private Button btnCancelar;
        private Button btnGuardar;
        private Label label1;
        private Label label2;
    }
}
