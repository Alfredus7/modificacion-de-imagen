using Accord.Imaging.Filters;
using System;
using System.Drawing;
using System.Windows.Forms;
using modificacion_de_imagen.clases;

namespace modificacion_de_imagen.MenusFiltros
{
    public partial class SaturacionDialog : Form
    {
        public AjustesImagen clase = new AjustesImagen();
        

        public SaturacionDialog(Bitmap image)
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
        private void Valorsaturacion_Scroll(object sender, EventArgs e)
        {
            labelvalor1.Text = TrackBackValor1.Value.ToString();
            aplicarFiltros();
        }

        private void aplicarFiltros()
        {
            if (originalImage == null) return;

            // Liberar la imagen anterior para evitar fugas de memoria
            ImageToProcess?.Dispose();

            // Enviar la misma instancia en lugar de crear un nuevo Bitmap
            ImageToProcess = clase.AplicarSaturacion(originalImage,TrackBackValor1.Value);

            pictureBoxPreview.Image = ImageToProcess;
        }
    }
}