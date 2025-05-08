using Accord.Imaging.Filters;
using System;
using System.Drawing;
using System.Windows.Forms;
using modificacion_de_imagen.clases;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Reflection.Emit;

namespace modificacion_de_imagen.MenusFiltros
{
    public partial class difusionAsinotropica : Form
    {
        public FiltrosPracticos2 clase = new FiltrosPracticos2();
        

        public difusionAsinotropica(Bitmap image)
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
        // Evento para el TrackBar de K
        private void trackBarK_Scroll(object sender, EventArgs e)
        {
            labelK.Text = "k: " + trackBarK.Value.ToString();
            aplicarFiltros();
        }

        // Evento para el NumericUpDown de Lambda
        private void numericUpDownLambda_ValueChanged(object sender, EventArgs e)
        {
            labelLambda.Text = "λ: " + numericUpDownLambda.Value.ToString("0.00"); // formato 0.00
            aplicarFiltros();
        }

        // Evento para el TrackBar de Iteraciones
        private void trackBarIteraciones_Scroll(object sender, EventArgs e)
        {
            labelIteraciones.Text = "Iteraciones: " + trackBarIteraciones.Value.ToString();
            aplicarFiltros();
        }


        private void aplicarFiltros()
        {
            if (originalImage == null) return;

            // Liberar la imagen anterior para evitar fugas de memoria
            ImageToProcess?.Dispose();
            int k = (int)trackBarK.Value; // K desde TrackBar
            double lambda = (double)numericUpDownLambda.Value; // Lambda desde NumericUpDown
            int iteraciones = (int)trackBarIteraciones.Value; // Iteraciones desde TrackBar
            // Enviar la misma instancia en lugar de crear un nuevo Bitmap
            ImageToProcess = clase.AplicarDifusionAnisotropica(originalImage, iteraciones, lambda, k);

            pictureBoxPreview.Image = ImageToProcess;
        }
    }
}