using System.Drawing;
using Accord.Imaging.Filters;

namespace modificacion_de_imagen.clases
{
    public class FiltrosBordesSuavizado
    {
        public Bitmap AplicarRealceBordes(Bitmap imagen) => new Sharpen().Apply(imagen);

        public Bitmap AplicarDesenfoqueBordes(Bitmap imagen) => new Blur().Apply(imagen);

        public Bitmap AplicarPasoAlto(Bitmap imagen, double sigma, int size)
        {
            if (size % 2 == 0) size++;
            if (size < 3) size = 3;

            return new GaussianSharpen(sigma, size).Apply(imagen);
        }

        public Bitmap AplicarPasoBajo(Bitmap imagen, double sigma, int size)
        {
            if (size % 2 == 0) size++;
            if (size < 3) size = 3;

            return new GaussianBlur(sigma, size).Apply(imagen);
        }
    }
}

