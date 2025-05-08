using Accord.Imaging.Filters;
using System;
using System.Drawing;
using System.Windows.Forms;
using modificacion_de_imagen.clases;

namespace modificacion_de_imagen.MenusFiltros
{
    public partial class PasoAltoDialog : Form
    {
        public FiltrosBordesSuavizado clase = new FiltrosBordesSuavizado();
        

        public PasoAltoDialog(Bitmap image)
        {
            InitializeComponent();
            originalImage = new Bitmap(image); // Guardamos la imagen original
            aplicarFiltro();
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
        private void trackBarSigma_Scroll(object sender, EventArgs e)
        {
            double sigma = trackBarSigma.Value / 10.0;
            labelSigmaValue.Text = $"{sigma:F1}";
            aplicarFiltro();
        }

        private void trackBarSize_Scroll(object sender, EventArgs e)
        {
            int size = trackBarSize.Value;
            // Asegurar que el tamaño sea impar
            if (size % 2 == 0) size += 1;
            labelSizeValue.Text = $"{size}";
            aplicarFiltro();
        }


        private void aplicarFiltro()
        {
            if (originalImage != null)
            {
                // Liberar la imagen anterior para evitar fugas de memoria
                ImageToProcess?.Dispose();
                ImageToProcess = clase.AplicarPasoAlto(new Bitmap(originalImage), trackBarSigma.Value, trackBarSize.Value);
                pictureBoxPreview.Image = ImageToProcess;
            }
        }

    }
}