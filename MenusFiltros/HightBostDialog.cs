using Accord.Imaging.Filters;
using System;
using System.Drawing;
using System.Windows.Forms;
using modificacion_de_imagen.clases;

namespace modificacion_de_imagen.MenusFiltros
{
    public partial class HightBostDialog : Form
    {
        public FiltrosPracticos2 clase = new FiltrosPracticos2();
        

        public HightBostDialog(Bitmap image)
        {
            InitializeComponent();
            originalImage = new Bitmap(image); // Guardamos la imagen original
            aplicarFiltros();
        }
        private void buttonApply_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }


        private Bitmap originalImage;
        public Bitmap ImageToProcess { get; private set; }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void aplicarFiltros()
        {
            if (originalImage == null) return;

            // Liberar la imagen anterior para evitar fugas de memoria
            ImageToProcess?.Dispose();

            // Enviar la misma instancia en lugar de crear un nuevo Bitmap
            ImageToProcess = clase.AplicarHighBoost(originalImage,(double)numericUpDown1.Value);

            pictureBoxPreview.Image = ImageToProcess;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            labelvalor1.Text = numericUpDown1.Value.ToString();
            aplicarFiltros();
        }
    }
}