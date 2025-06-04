using System;
using System.Drawing;
using System.Windows.Forms;
using modificacion_de_imagen.clases;

namespace modificacion_de_imagen.MenusFiltros
{
    public partial class DetectorCalcificationDialog : Form
    {
        public int UmbralValue { get; private set; }
        public Bitmap ImageToProcess { get; private set; }

        private Bitmap originalImage;
        private MicrocalcificationDetector detector = new MicrocalcificationDetector();

        public DetectorCalcificationDialog(Bitmap image)
        {
            InitializeComponent();
            originalImage = new Bitmap(image); // Guardamos la imagen original
            aplicarFiltro();
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            UmbralValue = (int)numericUpDownValor.Value;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void TrackBackvalor_Scroll(object sender, EventArgs e)
        {
            numericUpDownValor.Value = TrackBackvalor.Value;
            aplicarFiltro();
        }

        private void numericUpDownValor_ValueChanged(object sender, EventArgs e)
        {
            TrackBackvalor.Value = Convert.ToInt32(numericUpDownValor.Value);
            aplicarFiltro();
        }

        private void numericUpDownValor_Leave(object sender, EventArgs e)
        {
            if (int.TryParse(numericUpDownValor.Text, out int value))
            {
                TrackBackvalor.Value = Math.Max(TrackBackvalor.Minimum, Math.Min(TrackBackvalor.Maximum, value));
                aplicarFiltro();
            }
        }

        private void aplicarFiltro()
        {
            if (originalImage != null)
            {
                // Liberar memoria previa
                ImageToProcess?.Dispose();

                // Aplicar procesamiento con umbral fijo y erosión
                int umbral = (int)numericUpDownValor.Value;
                ImageToProcess = detector.Procesar(new Bitmap(originalImage), umbral);

                // Mostrar imagen procesada
                pictureBoxPreview.Image = ImageToProcess;
            }
        }
    }
}
