using System;
using System.Drawing;
using System.Windows.Forms;
using modificacion_de_imagen.clases;

namespace modificacion_de_imagen.MenusFiltros
{
    public partial class UmbralDialog : Form
    {
        public int UmbralValue { get; private set; }
        public Bitmap ImageToProcess { get; private set; }
        public FiltrosGrisesYUmbral clase = new FiltrosGrisesYUmbral();
        private Bitmap originalImage;

        public UmbralDialog(Bitmap image)
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
            // Solo actualizar el TrackBar cuando el usuario termina de escribir
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
                // Liberar la imagen anterior para evitar fugas de memoria
                ImageToProcess?.Dispose();
                ImageToProcess = clase.AplicarUmbral(new Bitmap(originalImage), (int)numericUpDownValor.Value);
                pictureBoxPreview.Image = ImageToProcess;
            }
        }
    }
}