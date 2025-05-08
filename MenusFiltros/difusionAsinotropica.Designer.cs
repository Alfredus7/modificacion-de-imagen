namespace modificacion_de_imagen.MenusFiltros
{
    partial class difusionAsinotropica
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
            this.labelFiltro = new System.Windows.Forms.Label();
            this.labelIteraciones = new System.Windows.Forms.Label();
            this.labelK = new System.Windows.Forms.Label();
            this.numericUpDownLambda = new System.Windows.Forms.NumericUpDown();
            this.labelLambda = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.trackBarK = new System.Windows.Forms.NumericUpDown();
            this.trackBarIteraciones = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLambda)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarIteraciones)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor = System.Drawing.Color.Gray;
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonCancel.ForeColor = System.Drawing.SystemColors.Control;
            this.buttonCancel.Location = new System.Drawing.Point(306, 256);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 17;
            this.buttonCancel.Text = "Cancelar";
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonApply
            // 
            this.buttonApply.BackColor = System.Drawing.Color.Gray;
            this.buttonApply.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonApply.ForeColor = System.Drawing.SystemColors.Control;
            this.buttonApply.Location = new System.Drawing.Point(387, 256);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(75, 23);
            this.buttonApply.TabIndex = 18;
            this.buttonApply.Text = "Aplicar";
            this.buttonApply.UseVisualStyleBackColor = false;
            this.buttonApply.Click += new System.EventHandler(this.buttonApply_Click);
            // 
            // pictureBoxPreview
            // 
            this.pictureBoxPreview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pictureBoxPreview.BackgroundImage = global::modificacion_de_imagen.Properties.Resources._1000_F_397645571_UaMOXS3wypnAHhJ3vBesqJmoeGSlmQ36;
            this.pictureBoxPreview.Location = new System.Drawing.Point(12, 24);
            this.pictureBoxPreview.Name = "pictureBoxPreview";
            this.pictureBoxPreview.Size = new System.Drawing.Size(260, 240);
            this.pictureBoxPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxPreview.TabIndex = 16;
            this.pictureBoxPreview.TabStop = false;
            // 
            // labelFiltro
            // 
            this.labelFiltro.AutoSize = true;
            this.labelFiltro.ForeColor = System.Drawing.SystemColors.Control;
            this.labelFiltro.Location = new System.Drawing.Point(303, 92);
            this.labelFiltro.Name = "labelFiltro";
            this.labelFiltro.Size = new System.Drawing.Size(85, 13);
            this.labelFiltro.TabIndex = 22;
            this.labelFiltro.Text = "Valor iteraciones";
            // 
            // labelIteraciones
            // 
            this.labelIteraciones.AutoSize = true;
            this.labelIteraciones.ForeColor = System.Drawing.SystemColors.Control;
            this.labelIteraciones.Location = new System.Drawing.Point(406, 108);
            this.labelIteraciones.Name = "labelIteraciones";
            this.labelIteraciones.Size = new System.Drawing.Size(19, 13);
            this.labelIteraciones.TabIndex = 24;
            this.labelIteraciones.Text = "15";
            // 
            // labelK
            // 
            this.labelK.AutoSize = true;
            this.labelK.ForeColor = System.Drawing.SystemColors.Control;
            this.labelK.Location = new System.Drawing.Point(406, 158);
            this.labelK.Name = "labelK";
            this.labelK.Size = new System.Drawing.Size(19, 13);
            this.labelK.TabIndex = 26;
            this.labelK.Text = "15";
            // 
            // numericUpDownLambda
            // 
            this.numericUpDownLambda.DecimalPlaces = 2;
            this.numericUpDownLambda.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDownLambda.Location = new System.Drawing.Point(306, 200);
            this.numericUpDownLambda.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numericUpDownLambda.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.numericUpDownLambda.Name = "numericUpDownLambda";
            this.numericUpDownLambda.ReadOnly = true;
            this.numericUpDownLambda.Size = new System.Drawing.Size(83, 20);
            this.numericUpDownLambda.TabIndex = 27;
            this.numericUpDownLambda.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDownLambda.Value = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            this.numericUpDownLambda.ValueChanged += new System.EventHandler(this.numericUpDownLambda_ValueChanged);
            // 
            // labelLambda
            // 
            this.labelLambda.AutoSize = true;
            this.labelLambda.ForeColor = System.Drawing.SystemColors.Control;
            this.labelLambda.Location = new System.Drawing.Point(406, 207);
            this.labelLambda.Name = "labelLambda";
            this.labelLambda.Size = new System.Drawing.Size(13, 13);
            this.labelLambda.TabIndex = 28;
            this.labelLambda.Text = "a";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(303, 134);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 29;
            this.label1.Text = "Valor K";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(303, 173);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 30;
            this.label2.Text = "Valor Lambda";
            // 
            // trackBarK
            // 
            this.trackBarK.DecimalPlaces = 2;
            this.trackBarK.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.trackBarK.Location = new System.Drawing.Point(305, 150);
            this.trackBarK.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.trackBarK.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.trackBarK.Name = "trackBarK";
            this.trackBarK.ReadOnly = true;
            this.trackBarK.Size = new System.Drawing.Size(83, 20);
            this.trackBarK.TabIndex = 31;
            this.trackBarK.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.trackBarK.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // trackBarIteraciones
            // 
            this.trackBarIteraciones.DecimalPlaces = 2;
            this.trackBarIteraciones.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.trackBarIteraciones.Location = new System.Drawing.Point(305, 108);
            this.trackBarIteraciones.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.trackBarIteraciones.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.trackBarIteraciones.Name = "trackBarIteraciones";
            this.trackBarIteraciones.ReadOnly = true;
            this.trackBarIteraciones.Size = new System.Drawing.Size(83, 20);
            this.trackBarIteraciones.TabIndex = 32;
            this.trackBarIteraciones.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.trackBarIteraciones.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // difusionAsinotropica
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(539, 315);
            this.Controls.Add(this.trackBarIteraciones);
            this.Controls.Add(this.trackBarK);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelLambda);
            this.Controls.Add(this.numericUpDownLambda);
            this.Controls.Add(this.labelK);
            this.Controls.Add(this.labelIteraciones);
            this.Controls.Add(this.labelFiltro);
            this.Controls.Add(this.buttonApply);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.pictureBoxPreview);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "difusionAsinotropica";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLambda)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarIteraciones)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonApply;
        private System.Windows.Forms.PictureBox pictureBoxPreview;
        private System.Windows.Forms.Label labelFiltro;
        private System.Windows.Forms.Label labelIteraciones;
        private System.Windows.Forms.Label labelK;
        private System.Windows.Forms.NumericUpDown numericUpDownLambda;
        private System.Windows.Forms.Label labelLambda;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown trackBarK;
        private System.Windows.Forms.NumericUpDown trackBarIteraciones;
    }
}