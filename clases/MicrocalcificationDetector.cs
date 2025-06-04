using System;
using System.Drawing;
using Accord.Imaging.Filters;

namespace modificacion_de_imagen.clases
{
    public class MicrocalcificationDetector
    {
        /// <summary>
        /// Procesa una imagen aplicando umbral y erosión para resaltar microcalcificaciones.
        /// Devuelve una imagen combinada lado a lado: [original en gris | erosionada].
        /// </summary>
        /// <param name="bitmap">Imagen original</param>
        /// <param name="umbralFijo">Valor del umbral (0-255)</param>
        /// <returns>Bitmap combinado</returns>
        public Bitmap Procesar(Bitmap bitmap, int umbralFijo)
        {
            // 1. Convertir a escala de grises
            Grayscale grayscaleFilter = new Grayscale(0.2125, 0.7154, 0.0721);
            Bitmap imagenGris = grayscaleFilter.Apply(bitmap);

            // 2. Aplicar umbral binario
            Threshold thresholdFilter = new Threshold(umbralFijo);
            Bitmap binarizada = thresholdFilter.Apply((Bitmap)imagenGris.Clone());

            // 3. Aplicar erosión (AForge no usa kernels personalizados fácilmente, usa predefined filters)
            Erosion erosionFilter = new Erosion();
            Bitmap erosionada = erosionFilter.Apply((Bitmap)binarizada.Clone());

            // 4. Crear imagen combinada
            int width = imagenGris.Width + erosionada.Width;
            int height = Math.Max(imagenGris.Height, erosionada.Height);
            Bitmap resultado = new Bitmap(width, height);

            using (Graphics g = Graphics.FromImage(resultado))
            {
                g.DrawImage(imagenGris, 0, 0);
                g.DrawImage(erosionada, imagenGris.Width, 0);
            }

            // 5. Devolver imagen combinada
            return resultado;
        }
    }
}


