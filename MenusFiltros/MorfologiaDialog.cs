using System;
using System.Drawing;
using System.Windows.Forms;
using modificacion_de_imagen.clases;

namespace modificacion_de_imagen.MenusFiltros
{
    public partial class MorfologiaDialog : Form
    {
        public int UmbralValue { get; private set; }
        public Bitmap ImageToProcess { get; private set; }

        private Bitmap originalImage;
        private OperacionesMorfologia detector = new OperacionesMorfologia();

        public MorfologiaDialog(Bitmap image)
        {
            InitializeComponent();
            originalImage = new Bitmap(image); // Guardamos la imagen original
            comboBoxOperacion.DataSource = Enum.GetValues(typeof(TipoOperacionMorfologica));
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
                ImageToProcess?.Dispose();

                int umbral = (int)numericUpDownValor.Value;

                // Detectar tipo de operación desde el ComboBox
                TipoOperacionMorfologica tipoOperacion = (TipoOperacionMorfologica)Enum.Parse(
                    typeof(TipoOperacionMorfologica),
                    comboBoxOperacion.SelectedItem.ToString()
                );

                ImageToProcess = detector.AplicarOperacionMorfologica(
                    new Bitmap(originalImage),
                    umbral,
                    tipoOperacion,
                    3
                );

                pictureBoxPreview.Image = ImageToProcess;
            }
        }

        private void comboBoxOperacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            aplicarFiltro();
        }
    }
}
