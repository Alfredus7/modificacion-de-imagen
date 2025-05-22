using System;
using System.Drawing;
using System.Drawing.Imaging;
using Accord.Imaging.Filters;

namespace modificacion_de_imagen.clases
{
    public class AjustesImagen
    {
        /// <summary>
        /// Aplica corrección de brillo y contraste a la imagen.
        /// </summary>
        public Bitmap AplicarBrilloContraste(Bitmap imagen, int brillo, int contraste)
        {
            var contrasteFiltro = new ContrastCorrection(contraste);
            var brilloFiltro = new BrightnessCorrection(brillo);

            Bitmap resultado = contrasteFiltro.Apply(imagen);
            return brilloFiltro.Apply(resultado);
        }

        /// <summary>
        /// Aplica corrección de saturación a la imagen.
        /// </summary>
        public Bitmap AplicarSaturacion(Bitmap imagen, int valor)
        {
            var saturacionFiltro = new SaturationCorrection(valor);
            return saturacionFiltro.Apply(imagen);
        }

        /// <summary>
        /// Aplica ecualización de histograma a la imagen.
        /// </summary>
        public Bitmap AplicarHistogramaEcualizacion(Bitmap imagen)
        {
            var histogramaFiltro = new HistogramEqualization();
            return histogramaFiltro.Apply(imagen);
        }

        public Bitmap Aplicargamma(Bitmap original)
        {
            var gammaFiltro = new GammaCorrection(0.8);
            return gammaFiltro.Apply(original.Clone(new Rectangle(0, 0, original.Width, original.Height), PixelFormat.Format24bppRgb));
        }
        /// <summary>
        /// Redimensiona una imagen al tamaño especificado.
        /// </summary>
        public Bitmap RedimensionarImagen(Bitmap imagen, int ancho, int alto)
        {
            if (ancho <= 0 || alto <= 0) throw new ArgumentException("El ancho y alto deben ser mayores que cero.");

            var resizeFiltro = new ResizeBilinear(ancho, alto);
            return resizeFiltro.Apply(imagen);
        }
    }
}
