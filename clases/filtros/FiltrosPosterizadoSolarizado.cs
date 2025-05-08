using System.Drawing;
using Accord.Imaging.Filters;

namespace modificacion_de_imagen.clases
{
    public class FiltrosPosterizadoSolarizado
    {
        public Bitmap AplicarSamplePosterizado(Bitmap image)
        {
            return new SimplePosterization().Apply(image);
        }

        public Bitmap AplicarPosterizado(Bitmap image, int niveles)
        {
            if (niveles < 2) niveles = 2;

            byte[] map = new byte[256];
            for (int i = 0; i < 256; i++)
                map[i] = (byte)((i * niveles / 256) * (255 / (niveles - 1)));

            var filter = new ColorRemapping(map, map, map);
            Bitmap result = (Bitmap)image.Clone();
            filter.ApplyInPlace(result);
            return result;
        }

        public Bitmap AplicarSolarizado(Bitmap image, int threshold, bool invertirOscuros)
        {
            byte[] map = new byte[256];
            for (int i = 0; i < 256; i++)
            {
                map[i] = (byte)(invertirOscuros ? (i < threshold ? 255 - i : i) : (i > threshold ? 255 - i : i));
            }

            var filter = new ColorRemapping(map, map, map);
            Bitmap result = (Bitmap)image.Clone();
            filter.ApplyInPlace(result);
            return result;
        }

        public Bitmap AplicarMedia(Bitmap image)
        {
            Bitmap compatibleImage = Accord.Imaging.Image.Clone(image, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            return new Median(6).Apply(compatibleImage);
        }
    }
}