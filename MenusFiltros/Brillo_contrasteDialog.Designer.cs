namespace modificacion_de_imagen.MenusFiltros
{
    partial class Brillo_contrasteDialog
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.TrackBackvalor = new System.Windows.Forms.TrackBar();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonApply = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBoxPreview = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TrackBackValor1 = new System.Windows.Forms.TrackBar();
            this.labelvalor = new System.Windows.Forms.Label();
            this.labelvalor1 = new System.Windows.Forms.Label();
            this.HistogramaChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBackvalor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBackValor1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HistogramaChart)).BeginInit();
            this.SuspendLayout();
            // 
            // TrackBackvalor
            // 
            this.TrackBackvalor.Location = new System.Drawing.Point(289, 242);
            this.TrackBackvalor.Maximum = 100;
            this.TrackBackvalor.Minimum = -100;
            this.TrackBackvalor.Name = "TrackBackvalor";
            this.TrackBackvalor.Size = new System.Drawing.Size(289, 45);
            this.TrackBackvalor.TabIndex = 14;
            this.TrackBackvalor.TickStyle = System.Windows.Forms.TickStyle.None;
            this.TrackBackvalor.Scroll += new System.EventHandler(this.valorBrilo_Scroll);
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor = System.Drawing.Color.Gray;
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonCancel.ForeColor = System.Drawing.SystemColors.Control;
            this.buttonCancel.Location = new System.Drawing.Point(390, 334);
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
            this.buttonApply.Location = new System.Drawing.Point(491, 334);
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
            this.label1.Location = new System.Drawing.Point(300, 226);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = "Valor brillo";
            // 
            // pictureBoxPreview
            // 
            this.pictureBoxPreview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pictureBoxPreview.BackgroundImage = global::modificacion_de_imagen.Properties.Resources._1000_F_397645571_UaMOXS3wypnAHhJ3vBesqJmoeGSlmQ36;
            this.pictureBoxPreview.Location = new System.Drawing.Point(12, 29);
            this.pictureBoxPreview.Name = "pictureBoxPreview";
            this.pictureBoxPreview.Size = new System.Drawing.Size(271, 287);
            this.pictureBoxPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxPreview.TabIndex = 16;
            this.pictureBoxPreview.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(300, 267);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Valor contraste";
            // 
            // TrackBackValor1
            // 
            this.TrackBackValor1.Location = new System.Drawing.Point(289, 283);
            this.TrackBackValor1.Maximum = 100;
            this.TrackBackValor1.Minimum = -100;
            this.TrackBackValor1.Name = "TrackBackValor1";
            this.TrackBackValor1.Size = new System.Drawing.Size(289, 45);
            this.TrackBackValor1.TabIndex = 20;
            this.TrackBackValor1.TickStyle = System.Windows.Forms.TickStyle.None;
            this.TrackBackValor1.Scroll += new System.EventHandler(this.ValorContraste_Scroll);
            // 
            // labelvalor
            // 
            this.labelvalor.AutoSize = true;
            this.labelvalor.ForeColor = System.Drawing.SystemColors.Control;
            this.labelvalor.Location = new System.Drawing.Point(584, 248);
            this.labelvalor.Name = "labelvalor";
            this.labelvalor.Size = new System.Drawing.Size(13, 13);
            this.labelvalor.TabIndex = 23;
            this.labelvalor.Text = "0";
            // 
            // labelvalor1
            // 
            this.labelvalor1.AutoSize = true;
            this.labelvalor1.ForeColor = System.Drawing.SystemColors.Control;
            this.labelvalor1.Location = new System.Drawing.Point(584, 289);
            this.labelvalor1.Name = "labelvalor1";
            this.labelvalor1.Size = new System.Drawing.Size(13, 13);
            this.labelvalor1.TabIndex = 24;
            this.labelvalor1.Text = "0";
            // 
            // HistogramaChart
            // 
            this.HistogramaChart.AntiAliasing = System.Windows.Forms.DataVisualization.Charting.AntiAliasingStyles.None;
            this.HistogramaChart.BackColor = System.Drawing.Color.Silver;
            this.HistogramaChart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.HistogramaChart.BorderlineColor = System.Drawing.Color.Gray;
            chartArea1.BackColor = System.Drawing.Color.Silver;
            chartArea1.Name = "ChartArea1";
            this.HistogramaChart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.HistogramaChart.Legends.Add(legend1);
            this.HistogramaChart.Location = new System.Drawing.Point(289, 29);
            this.HistogramaChart.Name = "HistogramaChart";
            this.HistogramaChart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None;
            series1.ChartArea = "ChartArea1";
            series1.Color = System.Drawing.Color.Crimson;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            series2.ChartArea = "ChartArea1";
            series2.Color = System.Drawing.Color.LimeGreen;
            series2.Legend = "Legend1";
            series2.Name = "Series2";
            series3.ChartArea = "ChartArea1";
            series3.Color = System.Drawing.Color.Cyan;
            series3.Legend = "Legend1";
            series3.Name = "Series3";
            this.HistogramaChart.Series.Add(series1);
            this.HistogramaChart.Series.Add(series2);
            this.HistogramaChart.Series.Add(series3);
            this.HistogramaChart.Size = new System.Drawing.Size(331, 194);
            this.HistogramaChart.TabIndex = 26;
            this.HistogramaChart.Text = "Histograma";
            this.HistogramaChart.TextAntiAliasingQuality = System.Windows.Forms.DataVisualization.Charting.TextAntiAliasingQuality.Normal;
            // 
            // Brillo_contrasteDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(632, 386);
            this.Controls.Add(this.HistogramaChart);
            this.Controls.Add(this.labelvalor1);
            this.Controls.Add(this.labelvalor);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TrackBackValor1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonApply);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.pictureBoxPreview);
            this.Controls.Add(this.TrackBackvalor);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Brillo_contrasteDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.TrackBackvalor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBackValor1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HistogramaChart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TrackBar TrackBackvalor;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonApply;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBoxPreview;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar TrackBackValor1;
        private System.Windows.Forms.Label labelvalor;
        private System.Windows.Forms.Label labelvalor1;
        private System.Windows.Forms.DataVisualization.Charting.Chart HistogramaChart;
    }
}