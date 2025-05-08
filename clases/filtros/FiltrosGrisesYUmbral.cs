using System.Drawing;
using Accord.Imaging.Filters;

namespace modificacion_de_imagen.clases
{
    public class FiltrosGrisesYUmbral
    {
        public Bitmap AplicarEscalaGrises(Bitmap imagen)
        {
            Grayscale grayscaleFilter = new Grayscale(0.2125, 0.7154, 0.0721);
            return grayscaleFilter.Apply(imagen);
        }

        public Bitmap AplicarUmbral(Bitmap imagen, int value)
        {
            Bitmap gray = AplicarEscalaGrises(imagen);
            return new Threshold(value).Apply(gray);
        }

        public Bitmap AplicarUmbralOtsu(Bitmap imagen)
        {
            Bitmap gray = AplicarEscalaGrises(imagen);
            return new OtsuThreshold().Apply(gray);
        }
    }
}

