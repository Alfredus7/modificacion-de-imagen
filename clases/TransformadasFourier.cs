using Accord.Imaging;
using Accord.Imaging.Filters;
using System;
using System.Drawing;
using System.Numerics;

namespace modificacion_de_imagen.clases
{
    public class TransformadasFourier
    {
        private ComplexImage imagenFourier;

        // Aplica FFT a la imagen
        public Bitmap AplicarTransformada(Bitmap imagenOriginal)
        {
            if (imagenOriginal == null)
                throw new ArgumentNullException(nameof(imagenOriginal));

            Grayscale grayscale = new Grayscale(0.2125, 0.7154, 0.0721);
            Bitmap gris = grayscale.Apply(imagenOriginal);

            imagenFourier = ComplexImage.FromBitmap(gris);
            imagenFourier.ForwardFourierTransform();

            return imagenFourier.ToBitmap(); // espectro de magnitud
        }

        // Aplica IFFT
        public Bitmap AplicarTransformadaInversa()
        {
            if (imagenFourier == null)
                throw new InvalidOperationException("No se ha aplicado la transformada de Fourier.");

            imagenFourier.BackwardFourierTransform();
            return imagenFourier.ToBitmap();
        }

        // Convolución en el dominio de la frecuencia (FFT)
        public Bitmap ConvolucionFFT(Bitmap imagenOriginal, Bitmap kernel)
        {
            Grayscale grayscale = new Grayscale(0.2125, 0.7154, 0.0721);
            Bitmap imagenGray = grayscale.Apply(imagenOriginal);
            Bitmap kernelGray = grayscale.Apply(kernel);

            ResizeBilinear resize = new ResizeBilinear(imagenGray.Width, imagenGray.Height);
            Bitmap kernelPadded = resize.Apply(kernelGray);

            ComplexImage imgFFT = ComplexImage.FromBitmap(imagenGray);
            imgFFT.ForwardFourierTransform();

            ComplexImage kernelFFT = ComplexImage.FromBitmap(kernelPadded);
            kernelFFT.ForwardFourierTransform();

            for (int y = 0; y < imgFFT.Height; y++)
            {
                for (int x = 0; x < imgFFT.Width; x++)
                {
                    imgFFT.Data[y, x] *= kernelFFT.Data[y, x];
                }
            }

            imgFFT.BackwardFourierTransform();

            // Normalización para evitar imagen oscura
            Bitmap result = imgFFT.ToBitmap();
            Bitmap normalized = new Bitmap(result.Width, result.Height);

            for (int y = 0; y < result.Height; y++)
            {
                for (int x = 0; x < result.Width; x++)
                {
                    Color pixel = result.GetPixel(x, y);
                    int r = Math.Min(255, Math.Max(0, (int)pixel.R));
                    normalized.SetPixel(x, y, Color.FromArgb(r, r, r));
                }
            }

            return normalized;
        }

        // Convolución directa con kernel 3x3
        public Bitmap ConvolucionDirecta(Bitmap imagenOriginal)
        {
            int[,] kernel = new int[,]
            {
            { 0, -1,  0 },
            { -1, 5, -1 },
            { 0, -1,  0 }
            };

            Convolution conv = new Convolution(kernel);
            return conv.Apply(imagenOriginal);
        }

        // Convierte kernel a imagen (con centrado y zero-padding)
        public Bitmap CrearImagenDesdeKernel(float[,] kernel, int width, int height)
        {
            Bitmap kernelImg = new Bitmap(width, height);
            int offsetX = width / 2 - kernel.GetLength(1) / 2;
            int offsetY = height / 2 - kernel.GetLength(0) / 2;

            for (int y = 0; y < kernel.GetLength(0); y++)
            {
                for (int x = 0; x < kernel.GetLength(1); x++)
                {
                    float valor = kernel[y, x];
                    int gris = (int)(valor * 127 + 128); // escala [-1,1] a [0,255]
                    gris = Math.Min(255, Math.Max(0, gris));
                    kernelImg.SetPixel(offsetX + x, offsetY + y, Color.FromArgb(gris, gris, gris));
                }
            }

            return kernelImg;
        }

        // Compara convolución directa vs por frecuencia
        public Bitmap CompararConvoluciones(Bitmap imagenOriginal)
        {
            Bitmap directa = ConvolucionDirecta(imagenOriginal);

            float[,] kernel = new float[,]
            {
            { 0, -1,  0 },
            { -1, 5, -1 },
            { 0, -1,  0 }
            };

            Bitmap kernelImg = CrearImagenDesdeKernel(kernel, imagenOriginal.Width, imagenOriginal.Height);
            Bitmap frecuencia = ConvolucionFFT(imagenOriginal, kernelImg); // <- Usar original
            Bitmap diferencia = new Bitmap(imagenOriginal.Width, imagenOriginal.Height);

            for (int y = 0; y < imagenOriginal.Height; y++)
            {
                for (int x = 0; x < imagenOriginal.Width; x++)
                {
                    Color color1 = directa.GetPixel(x, y);
                    Color color2 = frecuencia.GetPixel(x, y);

                    int diff = Math.Abs(color1.R - color2.R);
                    diff = Math.Min(255, diff);

                    diferencia.SetPixel(x, y, Color.FromArgb(diff, diff, diff));
                }
            }

            return UnirHorizontalmente(imagenOriginal, directa, frecuencia, diferencia);
        }


        public Bitmap UnirHorizontalmente(Bitmap original,Bitmap img1, Bitmap img2, Bitmap img3)
        {
            int width = original.Width+img1.Width + img2.Width + img3.Width;
            int height = Math.Max(img1.Height, Math.Max(img2.Height, img3.Height));

            Bitmap resultado = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(resultado))
            {
                g.Clear(Color.Black);
                g.DrawImage(original,0,0);
                g.DrawImage(img1, img1.Width, 0);
                g.DrawImage(img2, img1.Width + img2.Width, 0);
                g.DrawImage(img3, img1.Width + img2.Width+ img3.Width, 0);
            }

            return resultado;
        }

    }
}
