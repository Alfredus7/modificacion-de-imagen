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
        public Bitmap DetectarAutos(Bitmap bitmapOriginal, out Bitmap imagenProcesada,
    int cannyMin, int cannyMax, double areaMin, double areaMax)
        {
            Image<Bgr, byte> imgOriginal = bitmapOriginal.ToImage<Bgr, byte>();
            Image<Bgr, byte> imgSuavizada = imgOriginal.SmoothGaussian(5);
            Image<Gray, byte> imgGris = imgSuavizada.Convert<Gray, byte>();
            Image<Gray, byte> bordes = imgGris.Canny(cannyMin, cannyMax);

            Mat kernel = CvInvoke.GetStructuringElement(ElementShape.Rectangle, new Size(3, 3), new Point(-1, -1));
            CvInvoke.MorphologyEx(bordes, bordes, MorphOp.Close, kernel, new Point(-1, -1), 1, BorderType.Default, new MCvScalar());

            VectorOfVectorOfPoint contornos = new VectorOfVectorOfPoint();
            Mat jerarquia = new Mat();
            CvInvoke.FindContours(bordes, contornos, jerarquia, RetrType.External, ChainApproxMethod.ChainApproxSimple);

            int contador = 0;
            for (int i = 0; i < contornos.Size; i++)
            {
                double area = CvInvoke.ContourArea(contornos[i]);
                if (area > areaMin && area < areaMax)
                {
                    Rectangle rect = CvInvoke.BoundingRectangle(contornos[i]);
                    double aspecto = (double)rect.Width / rect.Height;
                    double areaRect = rect.Width * rect.Height;
                    double proporcionRelleno = area / areaRect;

                    var subImagen = imgGris.GetSubRect(rect);
                    MCvScalar media = CvInvoke.Mean(subImagen);
                    if (media.V0 > 220) continue;

                    if (aspecto > 0.4 && aspecto < 4.5 && proporcionRelleno > 0.2)
                    {
                        contador++;
                        CvInvoke.Rectangle(imgOriginal, rect, new MCvScalar(0, 255, 0), 2);
                    }
                }
            }

            imagenProcesada = imgOriginal.ToBitmap();
            imagenProcesada.Tag = contador; // Para que lo puedas mostrar fácilmente
            return imagenProcesada;
        }


    }
}


