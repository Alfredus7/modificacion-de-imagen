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
        /// ✨ Detecta y cuenta autos en una imagen aérea tipo Bitmap (por ejemplo desde un PictureBox).
        /// 💡 Permite definir bordes Canny y dimensiones mínimas y máximas (ancho y alto) para filtrar los autos detectados.
        /// </summary>
        /// <param name="bitmapOriginal">Imagen original en formato Bitmap (uwu)</param>
        /// <param name="imagenProcesada">Imagen de salida con autos detectados (con contornos dibujados OwO)</param>
        /// <param name="cannyMin">Valor mínimo para el detector de bordes Canny</param>
        /// <param name="cannyMax">Valor máximo para el detector de bordes Canny</param>
        /// <param name="anchoMax">Ancho máximo permitido para un auto detectado</param>
        /// <param name="altoMax">Alto máximo permitido para un auto detectado</param>
        /// <returns>Imagen con los autos detectados dibujaditos en verde UwU</returns>
        public Bitmap DetectarAutos(Bitmap bitmapOriginal, out Bitmap imagenProcesada,
            int cannyMin, int cannyMax,
            int anchoMax, int altoMax)
        {
            // ╔══════════════════════════════════╗
            // ║       1. Preprocesamiento        ║
            // ╚══════════════════════════════════╝

            // Convertimos la imagen original de Bitmap a formato EmguCV BGR (｡♥‿♥｡)
            Image<Bgr, byte> imgOriginal = bitmapOriginal.ToImage<Bgr, byte>();

            // Aplicamos detector de bordes Canny directo sobre la imagen a color (¡cuidado, pica! ⚡)
            Image<Gray, byte> bordes = imgOriginal.Canny(cannyMin, cannyMax);

            // ╔══════════════════════════════════╗
            // ║   2. Operaciones morfológicas    ║
            // ╚══════════════════════════════════╝

            // Creamos un kernel rectangular 3x3 para cerrar huecos entre contornos (´｡• ω •｡`)
            Mat kernel = CvInvoke.GetStructuringElement(ElementShape.Rectangle, new Size(3, 3), new Point(-1, -1));

            // Aplicamos una operación "Close" para unir fragmentos de borde =^.^=
            CvInvoke.MorphologyEx(bordes, bordes, MorphOp.Close, kernel, new Point(-1, -1), 1, BorderType.Default, new MCvScalar());

            // ╔══════════════════════════════════╗
            // ║       3. Detección de autos      ║
            // ╚══════════════════════════════════╝

            // Creamos estructuras para guardar los contornos encontrados (ꈍᴗꈍ)
            VectorOfVectorOfPoint contornos = new VectorOfVectorOfPoint();
            Mat jerarquia = new Mat();

            // Buscamos los contornos exteriores detectados en los bordes UwU
            CvInvoke.FindContours(bordes, contornos, jerarquia, RetrType.External, ChainApproxMethod.ChainApproxSimple);

            int contador = 0; // Contador kawaii de autos detectados ✧

            // Recorremos cada contorno detectado
            for (int i = 0; i < contornos.Size; i++)
            {
                double area = CvInvoke.ContourArea(contornos[i]);

                // Filtramos por un rango de área válido (ni muy chiquito, ni gigante ✨)
                if (area > 1400 && area < 20000)
                {
                    Rectangle rect = CvInvoke.BoundingRectangle(contornos[i]);

                    // Verificamos que el ancho y alto estén dentro de los límites dados por el usuario
                    if (rect.Width > anchoMax ||
                        rect.Height > altoMax)
                    {
                        continue; // No cumple con las medidas, lo ignoramos (；￣Д￣)
                    }

                    // Calculamos el aspecto (proporción ancho/alto) y proporción relleno
                    double aspecto = (double)rect.Width / rect.Height;
                    double areaRect = rect.Width * rect.Height;
                    double proporcionRelleno = area / areaRect;

                    // Tomamos la subimagen del rectángulo detectado
                    var subImagen = imgOriginal.GetSubRect(rect);
                    MCvScalar media = CvInvoke.Mean(subImagen);

                    // Si es muy clara, probablemente no sea auto (tal vez reflejo o techo blanco o_o)
                    if (media.V0 > 220) continue;

                    // Validamos forma y relleno para asegurar que parece un auto (o algo con ruedas :3)
                    if (aspecto > 0.4 && aspecto < 4.5 && proporcionRelleno > 0.2)
                    {
                        contador++; // ¡Auto detectado! \(★ω★)/

                        // Dibujamos un rectángulo verde en la imagen original
                        CvInvoke.Rectangle(imgOriginal, rect, new MCvScalar(0, 255, 0), 2);
                    }
                }
            }

            // ╔══════════════════════════════════╗
            // ║        4. Resultado final        ║
            // ╚══════════════════════════════════╝

            // Convertimos la imagen procesada a Bitmap para devolverla al mundo exterior ૮₍ ´• ˕ •` ₎ა
            imagenProcesada = imgOriginal.ToBitmap();

            // Guardamos el número de autos detectados como Tag (dato adicional accesible desde la imagen OwO)
            imagenProcesada.Tag = contador;

            // Retornamos la imagen bonita con los autos marcaditos 💚
            return imagenProcesada;
        }
    }
}
