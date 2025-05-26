using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Emgu.CV.CvEnum;

namespace modificacion_de_imagen.clases
{
    public class DetectorAutos
    {
        public Bitmap DetectarAutos(Bitmap bitmapOriginal, out Bitmap imagenProcesada,
            int cannyMin, int cannyMax,
            int anchoMax, int altoMax)
        {
            // ╔══════════════════════════════════╗
            // ║       1. Preprocesamiento        ║
            // ╚══════════════════════════════════╝
            Image<Bgr, byte> imgOriginal = bitmapOriginal.ToImage<Bgr, byte>();
            Image<Gray, byte> bordes = imgOriginal.Canny(cannyMin, cannyMax);

            // ╔══════════════════════════════════════════╗
            // ║   2. Operaciones morfológicas kawaii     ║
            // ╚══════════════════════════════════════════╝
            Mat kernel = CvInvoke.GetStructuringElement(ElementShape.Rectangle, new Size(3, 3), new Point(-1, -1));
            CvInvoke.MorphologyEx(bordes, bordes, MorphOp.Close, kernel, new Point(-1, -1), 1, BorderType.Default, new MCvScalar());

            // ╔═════════════════════════════════════════════╗
            // ║       3. Contornos y validación uwu         ║
            // ╚═════════════════════════════════════════════╝
            VectorOfVectorOfPoint contornos = new VectorOfVectorOfPoint();
            Mat jerarquia = new Mat();
            CvInvoke.FindContours(bordes, contornos, jerarquia, RetrType.External, ChainApproxMethod.ChainApproxSimple);

            int contador = 0;

            for (int i = 0; i < contornos.Size; i++)
            {
                Rectangle rect = CvInvoke.BoundingRectangle(contornos[i]);
                if (rect.Width > anchoMax || rect.Height > altoMax) continue;

                bool esBorde = rect.X <= 5 || rect.Y <= 5 || rect.Right >= imgOriginal.Width - 5 || rect.Bottom >= imgOriginal.Height - 5;
                
                double area = CvInvoke.ContourArea(contornos[i]);
                if (!esBorde && (area < 2000 || area > 11000)) continue;

                double aspecto = (double)rect.Width / rect.Height;
                if (aspecto < 0.1 || aspecto > 5.5) continue;

                using (var subImagen = imgOriginal.GetSubRect(rect))
                {
                    CvInvoke.Mean(subImagen); // 💡 Se usa solo por efectos laterales, pero no guardamos el brillo.
                    CvInvoke.Rectangle(imgOriginal, rect, new MCvScalar(0, 255, 0), 2);
                    contador++;
                }
            }

            // ╔════════════════════════════════════╗
            // ║        4. Resultado Final uwu      ║
            // ╚════════════════════════════════════╝
            imagenProcesada = imgOriginal.ToBitmap();
            imagenProcesada.Tag = contador;

            return imagenProcesada;
        }
    }
}
