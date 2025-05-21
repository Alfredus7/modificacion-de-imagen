using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace modificacion_de_imagen.MenusFiltros
{
    public partial class FormChebyshev : Form
    {
        public Bitmap ImageToProcess { get; private set; }

        private Bitmap originalImage;
        private Point puntoConsulta;
        List<Point> poblaciones = new List<Point>();

        public FormChebyshev(Bitmap image)
        {
            InitializeComponent();
            originalImage = new Bitmap(image);
            pictureBoxPreview.Image = new Bitmap(originalImage);
            DetectarPuntos(originalImage);
            calcularDistanciaChebyshev(); // mostrar al iniciar
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
            calcularDistanciaChebyshev();
        }

        private void numericUpDownValor_ValueChanged(object sender, EventArgs e)
        {
            TrackBackvalor.Value = Convert.ToInt32(numericUpDownValor.Value);
            calcularDistanciaChebyshev();
        }

        private void numericUpDownValor_Leave(object sender, EventArgs e)
        {
            if (int.TryParse(numericUpDownValor.Text, out int value))
            {
                TrackBackvalor.Value = Math.Max(TrackBackvalor.Minimum, Math.Min(TrackBackvalor.Maximum, value));
                calcularDistanciaChebyshev();
            }
        }

        private void calcularDistanciaChebyshev()
        {
            if (originalImage == null || puntoConsulta == Point.Empty || poblaciones.Count == 0)
                return;

            int cuadricula = (int)numericUpDownValor.Value;

            // Coordenadas en cuadrícula
            Point pConsultaGrid = new Point(puntoConsulta.X / cuadricula, puntoConsulta.Y / cuadricula);

            double distanciaMin = double.MaxValue;
            Point puntoMasCercano = Point.Empty;
            Point pGridMasCercano = Point.Empty;

            foreach (var p in poblaciones)
            {
                Point pGrid = new Point(p.X / cuadricula, p.Y / cuadricula);

                double distancia = Math.Max(
                    Math.Abs(pGrid.X - pConsultaGrid.X),
                    Math.Abs(pGrid.Y - pConsultaGrid.Y)
                );

                if (distancia < distanciaMin)
                {
                    distanciaMin = distancia;
                    puntoMasCercano = p;
                    pGridMasCercano = pGrid;
                }
            }

            lbdistancia.Text = $"Distancia Chebyshev: {distanciaMin}";

            ImageToProcess?.Dispose();
            ImageToProcess = new Bitmap(originalImage);

            using (Graphics g = Graphics.FromImage(ImageToProcess))
            {
                Pen rojo = new Pen(Color.Red, 3);
                Pen azul = new Pen(Color.DarkBlue, 1);

                Brush azulClaro = new SolidBrush(Color.FromArgb(100, 135, 206, 250));     // 💙 Azul clarito
                Brush amarilloClaro = new SolidBrush(Color.FromArgb(100, 255, 255, 128)); // 💛 Amarillo claro

                // 🧩 Dibujar cuadrícula
                for (int x = 0; x < ImageToProcess.Width; x += cuadricula)
                    g.DrawLine(azul, x, 0, x, ImageToProcess.Height);

                for (int y = 0; y < ImageToProcess.Height; y += cuadricula)
                    g.DrawLine(azul, 0, y, ImageToProcess.Width, y);

                // 🚶‍♂️ Simular movimiento del rey paso a paso
                Point actual = new Point(pConsultaGrid.X, pConsultaGrid.Y);
                Point destinoGrid = pGridMasCercano;

                bool azulTurno = true;

                while (actual != destinoGrid)
                {
                    Brush pasoBrush = azulTurno ? azulClaro : amarilloClaro;
                    g.FillRectangle(pasoBrush, actual.X * cuadricula, actual.Y * cuadricula, cuadricula, cuadricula);
                    azulTurno = !azulTurno;

                    int dx = destinoGrid.X - actual.X;
                    int dy = destinoGrid.Y - actual.Y;

                    if (dx != 0) actual.X += Math.Sign(dx);
                    if (dy != 0) actual.Y += Math.Sign(dy);
                }

                // ⭕ Dibujar círculo en el punto exacto más cercano
                g.DrawEllipse(rojo, puntoMasCercano.X - 5, puntoMasCercano.Y - 5, 10, 10);
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
