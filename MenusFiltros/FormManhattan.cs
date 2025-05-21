using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System;

namespace modificacion_de_imagen.MenusFiltros
{
    public partial class FormManhattan : Form
    {
        public Bitmap ImageToProcess { get; private set; }

        private Bitmap originalImage;
        private Point puntoConsulta;
        List<Point> poblaciones = new List<Point>();

        public FormManhattan(Bitmap image)
        {
            InitializeComponent();
            originalImage = new Bitmap(image);
            pictureBoxPreview.Image = new Bitmap(originalImage);
            DetectarPuntos(originalImage);
            CalcularDistanciaManhattan(); // mostrar al iniciar
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
            CalcularDistanciaManhattan();
        }

        private void numericUpDownValor_ValueChanged(object sender, EventArgs e)
        {
            TrackBackvalor.Value = Convert.ToInt32(numericUpDownValor.Value);
            CalcularDistanciaManhattan();
        }

        private void numericUpDownValor_Leave(object sender, EventArgs e)
        {
            if (int.TryParse(numericUpDownValor.Text, out int value))
            {
                TrackBackvalor.Value = Math.Max(TrackBackvalor.Minimum, Math.Min(TrackBackvalor.Maximum, value));
                CalcularDistanciaManhattan();
            }
        }

        private void CalcularDistanciaManhattan()
        {
            if (originalImage == null || puntoConsulta == Point.Empty || poblaciones.Count == 0)
                return;

            int cuadricula = (int)numericUpDownValor.Value;

            Point pConsultaGrid = new Point(
                puntoConsulta.X / cuadricula,
                puntoConsulta.Y / cuadricula
            );

            double distanciaMin = double.MaxValue;
            Point puntoMasCercano = Point.Empty;

            foreach (var p in poblaciones)
            {
                Point pGrid = new Point(p.X / cuadricula, p.Y / cuadricula);

                // Distancia Manhattan (✧ω✧)
                double distancia = Math.Abs(pGrid.X - pConsultaGrid.X) + Math.Abs(pGrid.Y - pConsultaGrid.Y);

                if (distancia < distanciaMin)
                {
                    distanciaMin = distancia;
                    puntoMasCercano = p;
                }
            }

            // Mostrar distancia kawaii ✿
            lbdistancia.Text = $"Distancia Manhattan: {distanciaMin}";

            // Redibujar imagen con cuadrícula y punto más cercano
            ImageToProcess?.Dispose();
            ImageToProcess = new Bitmap(originalImage);

            using (Graphics g = Graphics.FromImage(ImageToProcess))
            {
                Pen naranja = new Pen(Color.Orange, 3);
                Pen naranjaoscura = new Pen(Color.DarkOrange, 1);
                Pen camino = new Pen(Color.OrangeRed, 2); // 💖 Color kawaii para la ruta

                // 🌸 Dibujar la cuadrícula
                for (int x = 0; x < ImageToProcess.Width; x += cuadricula)
                    g.DrawLine(naranjaoscura, x, 0, x, ImageToProcess.Height);

                for (int y = 0; y < ImageToProcess.Height; y += cuadricula)
                    g.DrawLine(naranjaoscura, 0, y, ImageToProcess.Width, y);

                // 🎯 Dibujar el punto más cercano
                g.DrawEllipse(naranja, puntoMasCercano.X - 5, puntoMasCercano.Y - 5, 10, 10);

                // 💫 Dibujar la ruta Manhattan: primero en X, luego en Y
                Point inicio = puntoConsulta;
                Point intermedio = new Point(puntoMasCercano.X, puntoConsulta.Y);
                Point destino = puntoMasCercano;

                // Paso 1: línea horizontal
                g.DrawLine(camino, inicio, intermedio);
                // Paso 2: línea vertical
                g.DrawLine(camino, intermedio, destino);
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
