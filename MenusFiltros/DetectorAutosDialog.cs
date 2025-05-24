using System;
using System.Drawing;
using System.Windows.Forms;
using modificacion_de_imagen.clases;

namespace modificacion_de_imagen.MenusFiltros
{
    public partial class DetectorAutosDialog : Form
    {
        private Bitmap originalImage;
        private DetectorAutos detector = new DetectorAutos();

        public Bitmap ImageToProcess { get; private set; }

        public DetectorAutosDialog(Bitmap image)
        {
            InitializeComponent();
            originalImage = new Bitmap(image);
            pictureBoxPreview.Image = originalImage;
        }

        private void btnDetectar_Click(object sender, EventArgs e)
        {
            if (originalImage == null) return;

            ImageToProcess?.Dispose();

            int cannyMin = (int)numericCannyMin.Value;
            int cannyMax = (int)numericCannyMax.Value;
            double areaMin = (double)numericAreaMin.Value;
            double areaMax = (double)numericAreaMax.Value;

            ImageToProcess = detector.DetectarAutos(
                originalImage, out Bitmap procesada,
                cannyMin, cannyMax, areaMin, areaMax);

            pictureBoxPreview.Image = procesada;
            lblConteo.Text = $"Autos detectados: {ImageToProcess.Tag}";
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
