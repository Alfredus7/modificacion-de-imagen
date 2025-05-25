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
        /// Permite definir tamaños mínimo y máximo de ancho y alto para los autos.
        /// </summary>
        public Bitmap DetectarAutos(Bitmap bitmapOriginal, out Bitmap imagenProcesada,
            int cannyMin, int cannyMax,
            int anchoMin, int anchoMax, int altoMin, int altoMax)
        {
            // 1. Preprocesamiento
            Image<Bgr, byte> imgOriginal = bitmapOriginal.ToImage<Bgr, byte>();
            Image<Gray, byte> bordes = imgOriginal.Canny(cannyMin, cannyMax);

            // 2. Operaciones morfológicas
            Mat kernel = CvInvoke.GetStructuringElement(ElementShape.Rectangle, new Size(3, 3), new Point(-1, -1));
            CvInvoke.MorphologyEx(bordes, bordes, MorphOp.Close, kernel, new Point(-1, -1), 1, BorderType.Default, new MCvScalar());

            // 3. Detección de autos
            VectorOfVectorOfPoint contornos = new VectorOfVectorOfPoint();
            Mat jerarquia = new Mat();
            CvInvoke.FindContours(bordes, contornos, jerarquia, RetrType.External, ChainApproxMethod.ChainApproxSimple);

            int contador = 0;

            for (int i = 0; i < contornos.Size; i++)
            {
                double area = CvInvoke.ContourArea(contornos[i]);

                if (area > 1400 && area < 20000)
                {
                    Rectangle rect = CvInvoke.BoundingRectangle(contornos[i]);

                    // Filtro por dimensiones
                    if (rect.Width < anchoMin || rect.Width > anchoMax ||
                        rect.Height < altoMin || rect.Height > altoMax)
                    {
                        continue;
                    }

                    double aspecto = (double)rect.Width / rect.Height;
                    double areaRect = rect.Width * rect.Height;
                    double proporcionRelleno = area / areaRect;

                    var subImagen = imgOriginal.GetSubRect(rect);
                    MCvScalar media = CvInvoke.Mean(subImagen);

                    if (media.V0 > 220) continue;

                    if (aspecto > 0.4 && aspecto < 4.5 && proporcionRelleno > 0.2)
                    {
                        contador++;
                        CvInvoke.Rectangle(imgOriginal, rect, new MCvScalar(0, 255, 0), 2);
                    }
                }
            }

            // 4. Resultado final con comparación visual
            Bitmap bmpCanny = bordes.ToBitmap();
            Bitmap bmpResultado = imgOriginal.ToBitmap();

            int collageWidth = bmpCanny.Width + bmpResultado.Width;
            int collageHeight = Math.Max(bmpCanny.Height, bmpResultado.Height);
            Bitmap collage = new Bitmap(collageWidth, collageHeight);

            using (Graphics g = Graphics.FromImage(collage))
            {
                g.Clear(Color.Black);
                g.DrawImage(bmpCanny, 0, 0);
                g.DrawImage(bmpResultado, bmpCanny.Width, 0);
            }

            collage.Tag = contador;
            imagenProcesada = collage;
            return collage;
        }
    }
}