using System;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Emgu.CV.CvEnum;

namespace modificacion_de_imagen.clases
{
    public class DetectorAutos
    {
        /// <summary>
        /// Detecta y cuenta autos en una imagen aérea tipo Bitmap (ej. desde PictureBox).
        /// </summary>
        public int DetectarAutos(Bitmap bitmapOriginal, out Bitmap imagenProcesada)
        {
            // Convertir de Bitmap a Image<Bgr, byte>
            Image<Bgr, byte> imgOriginal = bitmapOriginal.ToImage<Bgr, byte>();

            // 1. Suavizado
            Image<Bgr, byte> imgSuavizada = imgOriginal.SmoothGaussian(5);

            // 2. Escala de grises
            Image<Gray, byte> imgGris = imgSuavizada.Convert<Gray, byte>();

            // 3. Detección de bordes
            Image<Gray, byte> bordes = imgGris.Canny(50, 150);

            // 4. Cierre morfológico
            Mat kernel = CvInvoke.GetStructuringElement(ElementShape.Rectangle, new Size(3, 3), new Point(-1, -1));
            CvInvoke.MorphologyEx(bordes, bordes, MorphOp.Close, kernel, new Point(-1, -1), 1, BorderType.Default, new MCvScalar());

            // 5. Encontrar contornos
            VectorOfVectorOfPoint contornos = new VectorOfVectorOfPoint();
            Mat jerarquia = new Mat();
            CvInvoke.FindContours(bordes, contornos, jerarquia, RetrType.External, ChainApproxMethod.ChainApproxSimple);

            // 6. Contar autos
            int contador = 0;
            for (int i = 0; i < contornos.Size; i++)
            {
                double area = CvInvoke.ContourArea(contornos[i]);
                if (area > 1500 && area < 30000)
                {
                    Rectangle rect = CvInvoke.BoundingRectangle(contornos[i]);
                    double aspecto = (double)rect.Width / rect.Height;
                    double areaRect = rect.Width * rect.Height;
                    double proporcionRelleno = area / areaRect;

                    if (aspecto > 0.5 && aspecto < 3.5 && proporcionRelleno > 0.45)
                    {
                        contador++;
                        CvInvoke.Rectangle(imgOriginal, rect, new MCvScalar(0, 255, 0), 2);
                    }
                }


            }

            // Devolver imagen procesada como Bitmap
            imagenProcesada = imgOriginal.ToBitmap();
            return contador;
        }
    }
}


