using System.Drawing;
using System.Drawing.Imaging;
using Accord.Imaging.Filters;

namespace modificacion_de_imagen.clases
{
    internal class filtrosArtisticos
    {
        // Filtro de Pintura al Óleo
        public Bitmap PinturaOleo(Bitmap original)
        {
            OilPainting filtro = new OilPainting(1);
            return filtro.Apply(original.Clone(
                new Rectangle(0, 0, original.Width, original.Height), PixelFormat.Format24bppRgb)
             );
        }

        // Filtro de Pixelado
        public Bitmap Pixelar(Bitmap original)
        {
            Pixellate filtro = new Pixellate(16);
            return filtro.Apply(original.Clone(
                new Rectangle(0, 0, original.Width, original.Height),
                PixelFormat.Format24bppRgb
            ));
        }

        // Filtro Sepia
        public Bitmap Sepia(Bitmap imagenOriginal)
        {
            Sepia filtro = new Sepia();
            return filtro.Apply(imagenOriginal);
        }

        // Filtro de Ondas de Agua
        public Bitmap OndasDeAgua(Bitmap imagenOriginal)
        {
            WaterWave filtro = new WaterWave
            {
                HorizontalWavesCount = 5,
                HorizontalWavesAmplitude = 5,
                VerticalWavesCount = 5,
                VerticalWavesAmplitude = 5
            };
            return filtro.Apply(imagenOriginal);
        }

        // Filtro Jitter
        public Bitmap Jitter(Bitmap imagenOriginal)
        {
            Jitter filtro = new Jitter
            {
                Radius = 5
            };
            return filtro.Apply(imagenOriginal);
        }

        // NUEVO - Filtro Gamma Correction
        public Bitmap CorregirGamma(Bitmap imagenOriginal, double gamma = 1.5)
        {
            GammaCorrection filtro = new GammaCorrection(gamma);
            return filtro.Apply(imagenOriginal);
        }
    }
}

