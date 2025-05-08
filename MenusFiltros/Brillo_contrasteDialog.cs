using Accord.Imaging;
using Accord.Statistics.Visualizations;
using System;
using System.Drawing;
using System.Windows.Forms;
using Accord.Math;
using System.Windows.Forms.DataVisualization.Charting;
using modificacion_de_imagen.clases;

namespace modificacion_de_imagen.MenusFiltros
{
    public partial class Brillo_contrasteDialog : Form
    {
        public AjustesImagen clase = new AjustesImagen();
        

        public Brillo_contrasteDialog(Bitmap image)
        {
            InitializeComponent();
            originalImage = new Bitmap(image); // Guardamos la imagen original
            aplicarFiltros();
            MostrarHistograma();
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

        private void valorBrilo_Scroll(object sender, EventArgs e)
        {
            labelvalor.Text = TrackBackvalor.Value.ToString();
            aplicarFiltros();
        }
        private void ValorContraste_Scroll(object sender, EventArgs e)
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
            ImageToProcess = clase.AplicarBrilloContraste(originalImage,TrackBackvalor.Value,TrackBackValor1.Value);

            pictureBoxPreview.Image = ImageToProcess;
            MostrarHistograma();
        }


        private void MostrarHistograma()
        {
            if (ImageToProcess == null) return;

            ImageStatistics stat = new ImageStatistics(ImageToProcess);
            Histogram red = stat.Red;
            Histogram green = stat.Green;
            Histogram blue = stat.Blue;

            // Limpiar series previas
            HistogramaChart.Series.Clear();

            // Crear series para cada canal
            Series redSeries = new Series("Rojo")
            {
                ChartType = SeriesChartType.Line,
                Color = Color.Crimson,
                BorderWidth = 2
            };

            Series greenSeries = new Series("Verde")
            {
                ChartType = SeriesChartType.Line,
                Color = Color.LawnGreen,
                BorderWidth = 2
            };

            Series blueSeries = new Series("Azul")
            {
                ChartType = SeriesChartType.Line,
                Color = Color.Cyan,
                BorderWidth = 2
            };

            int maxVal = Math.Max(Math.Max(red.Values.Max(), green.Values.Max()), blue.Values.Max());

            for (int i = 0; i < 256; i++)
            {
                double normalizedRed = (red.Values[i] / (double)maxVal) * 100;
                double normalizedGreen = (green.Values[i] / (double)maxVal) * 100;
                double normalizedBlue = (blue.Values[i] / (double)maxVal) * 100;

                redSeries.Points.AddXY(i, normalizedRed);
                greenSeries.Points.AddXY(i, normalizedGreen);
                blueSeries.Points.AddXY(i, normalizedBlue);
            }

            // Agregar las series al Chart
            HistogramaChart.Series.Add(redSeries);
            HistogramaChart.Series.Add(greenSeries);
            HistogramaChart.Series.Add(blueSeries);

            //// Configurar ejes
            //HistogramaChart.ChartAreas[0].AxisX.Minimum = 0;
            //HistogramaChart.ChartAreas[0].AxisX.Maximum = 255;
            //HistogramaChart.ChartAreas[0].AxisY.Minimum = 0;
            //HistogramaChart.ChartAreas[0].AxisY.Maximum = 100;
            //HistogramaChart.ChartAreas[0].AxisX.Title = "Intensidad";
            //HistogramaChart.ChartAreas[0].AxisY.Title = "Frecuencia Normalizada";
        }

    }
}