using Accord.Math.Distances;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;


namespace modificacion_de_imagen.MenusFiltros
{
    public partial class FormEuclidiana : Form
    {
        public Bitmap ImageToProcess { get; private set; }

        private Bitmap originalImage;
        private Point puntoConsulta;
        List<Point> poblaciones = new List<Point>();

        public FormEuclidiana(Bitmap image)
        {
            InitializeComponent();
            originalImage = new Bitmap(image);
            pictureBoxPreview.Image = new Bitmap(originalImage);
            DetectarPuntos(originalImage);
            CalculoDistanciaEuclidiana(); // mostrar al iniciar
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
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
            CalculoDistanciaEuclidiana();
        }

        private void numericUpDownValor_ValueChanged(object sender, EventArgs e)
        {
            TrackBackvalor.Value = Convert.ToInt32(numericUpDownValor.Value);
            CalculoDistanciaEuclidiana();
        }

        private void numericUpDownValor_Leave(object sender, EventArgs e)
        {
            if (int.TryParse(numericUpDownValor.Text, out int value))
            {
                TrackBackvalor.Value = Math.Max(TrackBackvalor.Minimum, Math.Min(TrackBackvalor.Maximum, value));
                CalculoDistanciaEuclidiana();
            }
        }

private void CalculoDistanciaEuclidiana()
    {
        if (originalImage == null || puntoConsulta == Point.Empty || poblaciones.Count == 0)
            return;

        int rangoOndas = (int)numericUpDownValor.Value;

        // Convertir el punto de consulta a vector (nani!?)
        double[] vectorConsulta = { puntoConsulta.X, puntoConsulta.Y };

        var distanciaEuclidiana = new Euclidean();

        double distanciaMin = double.MaxValue;
        Point puntoMasCercano = Point.Empty;

        foreach (var p in poblaciones)
        {
            double[] vectorPoblacion = { p.X, p.Y };
            double distancia = distanciaEuclidiana.Distance(vectorConsulta, vectorPoblacion);

            if (distancia < distanciaMin)
            {
                distanciaMin = distancia;
                puntoMasCercano = p;
            }
        }

        // Mostrar resultado (´｡• ᵕ •｡`) ♡
        lbdistancia.Text = $"Distancia Euclidiana: {distanciaMin:F2}";

        // Redibuja el radar~ (＊◕ᴗ◕＊)
        ImageToProcess?.Dispose();
        ImageToProcess = new Bitmap(originalImage);

        using (Graphics g = Graphics.FromImage(ImageToProcess))
        {
            Pen green = new Pen(Color.Green, 3);
            Pen ondas = new Pen(Color.DarkGreen, 1);

            for (int r = rangoOndas; r < Math.Max(ImageToProcess.Width, ImageToProcess.Height); r += rangoOndas)
            {
                g.DrawEllipse(ondas, puntoConsulta.X - r, puntoConsulta.Y - r, r * 2, r * 2);
            }

            g.DrawEllipse(green, puntoMasCercano.X - 5, puntoMasCercano.Y - 5, 10, 10);
        }

        pictureBoxPreview.Image = ImageToProcess;
    }



    private void DetectarPuntos(Bitmap bmp)
        {
            poblaciones.Clear();
            puntoConsulta = Point.Empty;

            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    Color pixel = bmp.GetPixel(x, y);

                    if (pixel.R == 255 && pixel.G == 255 && pixel.B == 255)
                    {
                        poblaciones.Add(new Point(x, y));
                    }
                    else if (pixel.R == 0 && pixel.G == 0 && pixel.B == 255)
                    {
                        puntoConsulta = new Point(x, y);
                    }
                }
            }
        }
    }
}
