using System.Drawing;
using System.Drawing.Imaging;
using Accord.Imaging.Filters;

namespace modificacion_de_imagen.clases
{
    public class FiltrosColor
    {
        public Bitmap AplicarFalseColor(Bitmap original)
        {
            byte[] r, g, b;
            GenerateColorMapGradient(out r, out g, out b);
            return new ColorRemapping(r, g, b).Apply(original);
        }
        public void GenerateColorMapGradient(out byte[] red, out byte[] green, out byte[] blue)
        {
            red = new byte[256];
            green = new byte[256];
            blue = new byte[256];

            for (int i = 0; i < 256; i++)
            {
                double v = i / 255.0;
                Color c = GetJetColor(v);
                red[i] = c.R;
                green[i] = c.G;
                blue[i] = c.B;
            }
        }

        private Color GetJetColor(double v)
        {
            if (v < 0.125) return Interpolate(v, 0.0, 0.125, Color.FromArgb(0, 0, 128), Color.FromArgb(0, 0, 255));
            else if (v < 0.375) return Interpolate(v, 0.125, 0.375, Color.FromArgb(0, 0, 255), Color.FromArgb(0, 255, 255));
            else if (v < 0.625) return Interpolate(v, 0.375, 0.625, Color.FromArgb(0, 255, 255), Color.FromArgb(255, 255, 0));
            else if (v < 0.875) return Interpolate(v, 0.625, 0.875, Color.FromArgb(255, 255, 0), Color.FromArgb(255, 0, 0));
            else return Interpolate(v, 0.875, 1.0, Color.FromArgb(255, 0, 0), Color.FromArgb(128, 0, 0));
        }

        private Color Interpolate(double v, double start, double end, Color c1, Color c2)
        {
            double t = (v - start) / (end - start);
            byte r = (byte)(c1.R + (c2.R - c1.R) * t);
            byte g = (byte)(c1.G + (c2.G - c1.G) * t);
            byte b = (byte)(c1.B + (c2.B - c1.B) * t);
            return Color.FromArgb(r, g, b);
        }
        public Bitmap AplicarInvertir(Bitmap original)
        {
            // Crear el filtro Invert
            Invert filter = new Invert();
            // Aplicar el filtro a la imagen original
            return filter.Apply(original.Clone(new Rectangle(0, 0, original.Width, original.Height), PixelFormat.Format24bppRgb));  // La imagen ya ha sido modificada en el lugar
        }
    }
}