﻿using System.Collections.Generic;
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

            Point pConsultaCelda = new Point(
                puntoConsulta.X / cuadricula,
                puntoConsulta.Y / cuadricula
            );

            double distanciaMin = double.MaxValue;
            Point puntoMasCercanoCelda = Point.Empty;
            Point puntoMasCercanoReal = Point.Empty; // <- este es el nuevo

            foreach (var p in poblaciones)
            {
                Point pCelda = new Point(p.X / cuadricula, p.Y / cuadricula);
                double distancia = Math.Abs(pCelda.X - pConsultaCelda.X) + Math.Abs(pCelda.Y - pConsultaCelda.Y);

                if (distancia < distanciaMin)
                {
                    distanciaMin = distancia;
                    puntoMasCercanoCelda = pCelda;
                    puntoMasCercanoReal = p; // <- guardamos el punto original
                }
            }

            lbdistancia.Text = $"Distancia Manhattan: {distanciaMin}";

            ImageToProcess?.Dispose();
            ImageToProcess = new Bitmap(originalImage);

            using (Graphics g = Graphics.FromImage(ImageToProcess))
            {
                Pen naranja = new Pen(Color.Orange, 3);
                Pen naranjaOscura = new Pen(Color.DarkOrange, 1);
                Pen camino = new Pen(Color.OrangeRed, 2);

                // Dibujar cuadrícula
                for (int x = 0; x < ImageToProcess.Width; x += cuadricula)
                    g.DrawLine(naranjaOscura, x, 0, x, ImageToProcess.Height);

                for (int y = 0; y < ImageToProcess.Height; y += cuadricula)
                    g.DrawLine(naranjaOscura, 0, y, ImageToProcess.Width, y);

                // Ruta Manhattan desde el punto de consulta al punto más cercano (en base a celdas)
                Point inicio = new Point(pConsultaCelda.X * cuadricula, pConsultaCelda.Y * cuadricula);
                Point intermedio = new Point(puntoMasCercanoCelda.X * cuadricula, inicio.Y);
                Point destino = new Point(puntoMasCercanoCelda.X * cuadricula, puntoMasCercanoCelda.Y * cuadricula);

                g.DrawLine(camino, inicio, intermedio);
                g.DrawLine(camino, intermedio, destino);

                // 🎯 Ahora dibujamos el círculo directamente en el punto real más cercano
                int radio = 10;
                g.FillEllipse(Brushes.Orange, puntoMasCercanoReal.X - radio / 2, puntoMasCercanoReal.Y - radio / 2, radio, radio);
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
