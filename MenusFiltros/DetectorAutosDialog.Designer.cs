namespace modificacion_de_imagen.MenusFiltros
{
    partial class DetectorAutosDialog
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
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonApply = new System.Windows.Forms.Button();
            this.pictureBoxPreview = new System.Windows.Forms.PictureBox();
            this.lblConteo = new System.Windows.Forms.Label();
            this.numericCannyMin = new System.Windows.Forms.NumericUpDown();
            this.numericCannyMax = new System.Windows.Forms.NumericUpDown();
            this.btndetectar = new System.Windows.Forms.Button();
            this.numericMin_width = new System.Windows.Forms.NumericUpDown();
            this.numericMax_width = new System.Windows.Forms.NumericUpDown();
            this.numericMin_height = new System.Windows.Forms.NumericUpDown();
            this.numericMax_height = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericCannyMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericCannyMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMin_width)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMax_width)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMin_height)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMax_height)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor = System.Drawing.Color.Gray;
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonCancel.ForeColor = System.Drawing.SystemColors.Control;
            this.buttonCancel.Location = new System.Drawing.Point(504, 409);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 17;
            this.buttonCancel.Text = "Cancelar";
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // buttonApply
            // 
            this.buttonApply.BackColor = System.Drawing.Color.Gray;
            this.buttonApply.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonApply.ForeColor = System.Drawing.SystemColors.Control;
            this.buttonApply.Location = new System.Drawing.Point(599, 409);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(75, 23);
            this.buttonApply.TabIndex = 18;
            this.buttonApply.Text = "Aplicar";
            this.buttonApply.UseVisualStyleBackColor = false;
            this.buttonApply.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // pictureBoxPreview
            // 
            this.pictureBoxPreview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pictureBoxPreview.BackgroundImage = global::modificacion_de_imagen.Properties.Resources._1000_F_397645571_UaMOXS3wypnAHhJ3vBesqJmoeGSlmQ36;
            this.pictureBoxPreview.Location = new System.Drawing.Point(12, 24);
            this.pictureBoxPreview.Name = "pictureBoxPreview";
            this.pictureBoxPreview.Size = new System.Drawing.Size(486, 408);
            this.pictureBoxPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxPreview.TabIndex = 16;
            this.pictureBoxPreview.TabStop = false;
            // 
            // lblConteo
            // 
            this.lblConteo.AutoSize = true;
            this.lblConteo.ForeColor = System.Drawing.SystemColors.Control;
            this.lblConteo.Location = new System.Drawing.Point(596, 378);
            this.lblConteo.Name = "lblConteo";
            this.lblConteo.Size = new System.Drawing.Size(96, 13);
            this.lblConteo.TabIndex = 22;
            this.lblConteo.Text = "Autos detectados: ";
            // 
            // numericCannyMin
            // 
            this.numericCannyMin.DecimalPlaces = 1;
            this.numericCannyMin.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericCannyMin.Location = new System.Drawing.Point(572, 57);
            this.numericCannyMin.Maximum = new decimal(new int[] {
            150,
            0,
            0,
            0});
            this.numericCannyMin.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericCannyMin.Name = "numericCannyMin";
            this.numericCannyMin.ReadOnly = true;
            this.numericCannyMin.Size = new System.Drawing.Size(120, 20);
            this.numericCannyMin.TabIndex = 28;
            this.numericCannyMin.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // numericCannyMax
            // 
            this.numericCannyMax.DecimalPlaces = 1;
            this.numericCannyMax.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericCannyMax.Location = new System.Drawing.Point(572, 83);
            this.numericCannyMax.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.numericCannyMax.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericCannyMax.Name = "numericCannyMax";
            this.numericCannyMax.ReadOnly = true;
            this.numericCannyMax.Size = new System.Drawing.Size(120, 20);
            this.numericCannyMax.TabIndex = 27;
            this.numericCannyMax.Value = new decimal(new int[] {
            150,
            0,
            0,
            0});
            // 
            // btndetectar
            // 
            this.btndetectar.BackColor = System.Drawing.Color.Gray;
            this.btndetectar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btndetectar.ForeColor = System.Drawing.SystemColors.Control;
            this.btndetectar.Location = new System.Drawing.Point(504, 368);
            this.btndetectar.Name = "btndetectar";
            this.btndetectar.Size = new System.Drawing.Size(75, 23);
            this.btndetectar.TabIndex = 29;
            this.btndetectar.Text = "detectar";
            this.btndetectar.UseVisualStyleBackColor = false;
            this.btndetectar.Click += new System.EventHandler(this.btnDetectar_Click);
            // 
            // numericMin_width
            // 
            this.numericMin_width.DecimalPlaces = 1;
            this.numericMin_width.Location = new System.Drawing.Point(572, 135);
            this.numericMin_width.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericMin_width.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericMin_width.Name = "numericMin_width";
            this.numericMin_width.ReadOnly = true;
            this.numericMin_width.Size = new System.Drawing.Size(120, 20);
            this.numericMin_width.TabIndex = 31;
            this.numericMin_width.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // numericMax_width
            // 
            this.numericMax_width.DecimalPlaces = 1;
            this.numericMax_width.Location = new System.Drawing.Point(572, 161);
            this.numericMax_width.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericMax_width.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericMax_width.Name = "numericMax_width";
            this.numericMax_width.ReadOnly = true;
            this.numericMax_width.Size = new System.Drawing.Size(120, 20);
            this.numericMax_width.TabIndex = 30;
            this.numericMax_width.Value = new decimal(new int[] {
            180,
            0,
            0,
            0});
            // 
            // numericMin_height
            // 
            this.numericMin_height.DecimalPlaces = 1;
            this.numericMin_height.Location = new System.Drawing.Point(572, 222);
            this.numericMin_height.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericMin_height.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericMin_height.Name = "numericMin_height";
            this.numericMin_height.ReadOnly = true;
            this.numericMin_height.Size = new System.Drawing.Size(120, 20);
            this.numericMin_height.TabIndex = 33;
            this.numericMin_height.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // numericMax_height
            // 
            this.numericMax_height.DecimalPlaces = 1;
            this.numericMax_height.Location = new System.Drawing.Point(572, 248);
            this.numericMax_height.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericMax_height.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericMax_height.Name = "numericMax_height";
            this.numericMax_height.ReadOnly = true;
            this.numericMax_height.Size = new System.Drawing.Size(120, 20);
            this.numericMax_height.TabIndex = 32;
            this.numericMax_height.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(504, 206);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 13);
            this.label1.TabIndex = 34;
            this.label1.Text = "alto";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(504, 119);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 35;
            this.label2.Text = "ancho";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.Control;
            this.label3.Location = new System.Drawing.Point(504, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 36;
            this.label3.Text = "canny ";
            // 
            // DetectorAutosDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(709, 460);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericMin_height);
            this.Controls.Add(this.numericMax_height);
            this.Controls.Add(this.numericMin_width);
            this.Controls.Add(this.numericMax_width);
            this.Controls.Add(this.btndetectar);
            this.Controls.Add(this.numericCannyMin);
            this.Controls.Add(this.numericCannyMax);
            this.Controls.Add(this.lblConteo);
            this.Controls.Add(this.buttonApply);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.pictureBoxPreview);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DetectorAutosDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericCannyMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericCannyMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMin_width)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMax_width)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMin_height)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMax_height)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonApply;
        private System.Windows.Forms.PictureBox pictureBoxPreview;
        private System.Windows.Forms.Label lblConteo;
        private System.Windows.Forms.NumericUpDown numericCannyMin;
        private System.Windows.Forms.NumericUpDown numericCannyMax;
        private System.Windows.Forms.Button btndetectar;
        private System.Windows.Forms.NumericUpDown numericMin_width;
        private System.Windows.Forms.NumericUpDown numericMax_width;
        private System.Windows.Forms.NumericUpDown numericMin_height;
        private System.Windows.Forms.NumericUpDown numericMax_height;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}