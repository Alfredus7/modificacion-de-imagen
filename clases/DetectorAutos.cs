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
            // ╔══════════════════════════════════╗
            // ║       1. Preprocesamiento        ║
            // ╚══════════════════════════════════╝

            // Convertimos la imagen Bitmap a formato compatible con EmguCV (｡♥‿♥｡)
            Image<Bgr, byte> imgOriginal = bitmapOriginal.ToImage<Bgr, byte>();

            // Suavizamos la imagen para reducir ruido UwU
            Image<Bgr, byte> imgSuavizada = imgOriginal.SmoothGaussian(5);

            // La convertimos a escala de grises (más fácil para detectar bordes ☆彡)
            Image<Gray, byte> imgGris = imgSuavizada.Convert<Gray, byte>();

            // Aplicamos el detector de bordes Canny (¡cuidado! está afilado (>ω<)✧)
            Image<Gray, byte> bordes = imgGris.Canny(cannyMin, cannyMax);

            // ╔══════════════════════════════════╗
            // ║   2. Operaciones morfológicas    ║
            // ╚══════════════════════════════════╝

            // Creamos un kernel rectangular para cerrar huequitos (´｡• ω •｡`)
            Mat kernel = CvInvoke.GetStructuringElement(ElementShape.Rectangle, new Size(3, 3), new Point(-1, -1));

            // Aplicamos "Close" para unir bordes discontinuos =^.^=
            CvInvoke.MorphologyEx(bordes, bordes, MorphOp.Close, kernel, new Point(-1, -1), 1, BorderType.Default, new MCvScalar());

            // ╔══════════════════════════════════╗
            // ║       3. Detección de autos      ║
            // ╚══════════════════════════════════╝

            // Creamos lista para guardar los contornos encontrados (ꈍᴗꈍ)
            VectorOfVectorOfPoint contornos = new VectorOfVectorOfPoint();
            Mat jerarquia = new Mat();

            // Detectamos contornos externos (ideal para objetos aislados como autos UwU)
            CvInvoke.FindContours(bordes, contornos, jerarquia, RetrType.External, ChainApproxMethod.ChainApproxSimple);

            int contador = 0;

            for (int i = 0; i < contornos.Size; i++)
            {
                // Calculamos el área del contorno (´｡• ᵕ •｡`)
                double area = CvInvoke.ContourArea(contornos[i]);

                // Validamos si está dentro del rango deseado (*≧ω≦)
                if (area > areaMin && area < areaMax)
                {
                    // Obtenemos el rectángulo que encierra el contorno
                    Rectangle rect = CvInvoke.BoundingRectangle(contornos[i]);

                    // Calculamos proporciones para filtrar por forma y contenido
                    double aspecto = (double)rect.Width / rect.Height;
                    double areaRect = rect.Width * rect.Height;
                    double proporcionRelleno = area / areaRect;

                    // Extraemos la subimagen dentro del rectángulo
                    var subImagen = imgGris.GetSubRect(rect);
                    MCvScalar media = CvInvoke.Mean(subImagen);

                    // Si es muy brillante, probablemente no sea un auto (≧▽≦)
                    if (media.V0 > 220) continue;

                    // Validamos forma alargada/típica de autos
                    if (aspecto > 0.4 && aspecto < 4.5 && proporcionRelleno > 0.2)
                    {
                        contador++; // ¡Auto detectado! \(★ω★)/
                        CvInvoke.Rectangle(imgOriginal, rect, new MCvScalar(0, 255, 0), 2); // Dibujamos el contorno verde
                    }
                }
            }

            // ╔══════════════════════════════════╗
            // ║        4. Resultado final        ║
            // ╚══════════════════════════════════╝

            // Convertimos la imagen procesada a Bitmap para devolverla al mundo exterior ૮₍ ´• ˕ •` ₎ა
            imagenProcesada = imgOriginal.ToBitmap();

            // Guardamos el contador como tag de la imagen (*≧ω≦)
            imagenProcesada.Tag = contador;

            // Devolvemos la imagen con los autos detectados
            return imagenProcesada;
        }
    }
}

