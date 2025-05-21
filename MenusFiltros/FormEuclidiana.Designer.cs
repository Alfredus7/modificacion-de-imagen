namespace modificacion_de_imagen.MenusFiltros
{
    partial class FormEuclidiana
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
            this.TrackBackvalor = new System.Windows.Forms.TrackBar();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonApply = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBoxPreview = new System.Windows.Forms.PictureBox();
            this.numericUpDownValor = new System.Windows.Forms.NumericUpDown();
            this.lbdistancia = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBackvalor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownValor)).BeginInit();
            this.SuspendLayout();
            // 
            // TrackBackvalor
            // 
            this.TrackBackvalor.Location = new System.Drawing.Point(296, 131);
            this.TrackBackvalor.Maximum = 24;
            this.TrackBackvalor.Minimum = 1;
            this.TrackBackvalor.Name = "TrackBackvalor";
            this.TrackBackvalor.Size = new System.Drawing.Size(162, 45);
            this.TrackBackvalor.TabIndex = 14;
            this.TrackBackvalor.TickStyle = System.Windows.Forms.TickStyle.None;
            this.TrackBackvalor.Value = 4;
            this.TrackBackvalor.Scroll += new System.EventHandler(this.TrackBackvalor_Scroll);
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor = System.Drawing.Color.Gray;
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonCancel.ForeColor = System.Drawing.SystemColors.Control;
            this.buttonCancel.Location = new System.Drawing.Point(306, 240);
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
            this.buttonApply.Location = new System.Drawing.Point(401, 240);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(75, 23);
            this.buttonApply.TabIndex = 18;
            this.buttonApply.Text = "Aplicar";
            this.buttonApply.UseVisualStyleBackColor = false;
            this.buttonApply.Click += new System.EventHandler(this.buttonApply_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(303, 97);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = "Valor posterizado";
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
            // numericUpDownValor
            // 
            this.numericUpDownValor.BackColor = System.Drawing.Color.Gray;
            this.numericUpDownValor.ForeColor = System.Drawing.SystemColors.Control;
            this.numericUpDownValor.Location = new System.Drawing.Point(464, 131);
            this.numericUpDownValor.Maximum = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.numericUpDownValor.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownValor.Name = "numericUpDownValor";
            this.numericUpDownValor.Size = new System.Drawing.Size(41, 20);
            this.numericUpDownValor.TabIndex = 15;
            this.numericUpDownValor.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numericUpDownValor.ValueChanged += new System.EventHandler(this.numericUpDownValor_ValueChanged);
            this.numericUpDownValor.Leave += new System.EventHandler(this.numericUpDownValor_Leave);
            // 
            // lbdistancia
            // 
            this.lbdistancia.AutoSize = true;
            this.lbdistancia.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lbdistancia.Location = new System.Drawing.Point(303, 163);
            this.lbdistancia.Name = "lbdistancia";
            this.lbdistancia.Size = new System.Drawing.Size(35, 13);
            this.lbdistancia.TabIndex = 21;
            this.lbdistancia.Text = "label2";
            // 
            // FormEuclidiana
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(539, 315);
            this.Controls.Add(this.lbdistancia);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonApply);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.pictureBoxPreview);
            this.Controls.Add(this.numericUpDownValor);
            this.Controls.Add(this.TrackBackvalor);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormEuclidiana";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.TrackBackvalor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownValor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TrackBar TrackBackvalor;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonApply;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBoxPreview;
        private System.Windows.Forms.NumericUpDown numericUpDownValor;
        private System.Windows.Forms.Label lbdistancia;
    }
}