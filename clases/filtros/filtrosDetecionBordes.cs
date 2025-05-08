using Accord.Imaging.Filters;
using System.Drawing;
using System.Drawing.Imaging;

namespace modificacion_de_imagen.clases
{
    internal class filtrosDetecionBordes
    {
        // Filtro para convertir a escala de grises con proporciones humanamente kawaii (｡♥‿♥｡)
        private Grayscale grayscaleFilter = new Grayscale(0.2125, 0.7154, 0.0721);

        // 🌸 Método para aplicar el operador de Sobel (detecta bordes horizontales y verticales fuertes)
        public Bitmap AplicarSobel(Bitmap imagen)
        {
            SobelEdgeDetector filter = new SobelEdgeDetector();
            return filter.Apply(grayscaleFilter.Apply(imagen)); // Convertimos a grises antes de aplicar
        }

        // 🌸 Método para aplicar el detector de bordes Canny (súper preciso y elegante como una waifu ninja~)
        public Bitmap AplicarCanny(Bitmap imagen)
        {
            CannyEdgeDetector filter = new CannyEdgeDetector();
            return filter.Apply(grayscaleFilter.Apply(imagen)); // Imagen gris antes del filo afilado 💢
        }

        // 🌸 Método para aplicar el operador de Roberts usando máscaras extendidas de 3x3
        public Bitmap AplicarRoberts(Bitmap imagen)
        {
            // Máscara para el eje X del operador Roberts (chiquita pero peligrosa ≧◡≦)
            int[,] robertsGX = new int[,]
            {
                { 1, 0, 0 },
                { 0, -1, 0 },
                { 0, 0, 0 }
            };

            // Máscara para el eje Y del operador Roberts (¡compañera de ataque!)
            int[,] robertsGY = new int[,]
            {
                { 0, 1, 0 },
                { -1, 0, 0 },
                { 0, 0, 0 }
            };

            // Aplicamos cada convolución sobre la imagen gris 🌌
            Bitmap gx = new Convolution(robertsGX).Apply(grayscaleFilter.Apply(imagen));
            Bitmap gy = new Convolution(robertsGY).Apply(grayscaleFilter.Apply(imagen));

            // Combinamos las dos imágenes resultantes para ver todos los bordes posibles 💕
            Merge merge = new Merge(gx);
            Bitmap resultado = merge.Apply(gy);

            return resultado;
        }

        // 🌸 Método con filtro Difference (detecta cambios bruscos, como un plot twist en el anime)
        public Bitmap AplicarDifference(Bitmap imagen)
        {
            DifferenceEdgeDetector filter = new DifferenceEdgeDetector();
            return filter.Apply(grayscaleFilter.Apply(imagen));
        }

        // 🌸 Método con filtro Homogenity (detecta bordes por diferencia de intensidad suave~)
        public Bitmap AplicarHomogenity(Bitmap imagen)
        {
            HomogenityEdgeDetector filter = new HomogenityEdgeDetector();
            return filter.Apply(grayscaleFilter.Apply(imagen));
        }

        // 🌸 Método para aplicar un filtro Laplaciano clásico (¡ideal para encontrar contornos mágicos!)
        public Bitmap AplicarLaplaciano(Bitmap imagen)
        {
            // Kernel Laplaciano para resaltar áreas con cambios extremos de intensidad ⊂(◉‿◉)つ
            int[,] KernelLaplaciana = new int[,]
            {
                { 0, -1, 0 },
                { -1, 4, -1 },
                { 0, -1, 0 }
            };

            // Convertimos la imagen a gris antes de hacer magia con la matriz mística 🌙
            Bitmap imagenGris = grayscaleFilter.Apply(imagen);

            // Aplicamos la convolución Laplaciana ✨
            Convolution laplacianFilter = new Convolution(KernelLaplaciana);
            Bitmap resultado = laplacianFilter.Apply(imagenGris);

            // Estiramos el contraste para que los bordes se vean más intensos como una escena dramática~!
            ContrastStretch contraste = new ContrastStretch();
            contraste.ApplyInPlace(resultado);

            return resultado;
        }

        // 🌸 Método para aplicar Laplaciano del Gaussiano (LoG) – combina suavizado + bordes, ¡una dupla dinámica!
        public Bitmap AplicarLaplacianoGauss(Bitmap imagen)
        {
            // Suavizamos la imagen para quitar ruiditos molestos (σ = 1.4, recomendado por la ciencia y las maids~)
            GaussianBlur suavizado = new GaussianBlur(1.4, 5);

            // Pasos mágicos: gris → suavizar → aplicar Laplaciano (¡combo de hechizos mágicos! ★)
            Bitmap imagenSuavizada = suavizado.Apply(grayscaleFilter.Apply(imagen));
            Bitmap laplaciano = AplicarLaplaciano(imagenSuavizada.Clone(
                new Rectangle(0, 0, imagenSuavizada.Width, imagenSuavizada.Height),
                PixelFormat.Format24bppRgb));

            return laplaciano;
        }
    }
}


