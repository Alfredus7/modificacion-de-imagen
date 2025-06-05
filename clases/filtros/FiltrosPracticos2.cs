using System;
using System.Drawing;
using System.Drawing.Imaging;
using Accord.Imaging.Filters;

/// <summary>
/// Clase mágica de filtrosPractico2 prácticos para procesar imágenes.
/// Incluye: Laplaciano, Realce, High-Boost y Difusión Anisotrópica.
/// </summary>
public class FiltrosPracticos2
{
    // A) FILTRO DE REALCE + LAPLACIANO

    /// <summary>
    /// Aplica un filtro de Laplaciano 3x3 para resaltar bordes en la imagen.
    /// (・∀・)
    /// </summary>
    public Bitmap AplicarLaplaciano(Bitmap imagen)
    {
        // Kernel Laplaciana 3x3 que detecta cambios de intensidad (bordes)
        int[,] KernelLaplaciana = new int[,]
        {
            { 0, -1, 0 },
            { -1, 4, -1 },
            { 0, -1, 0 }
        };

        // Aplicar la convolución con la máscara
        Convolution laplacianFilter = new Convolution(KernelLaplaciana);
        Bitmap imagenLaplaciano = laplacianFilter.Apply(imagen);

        return imagenLaplaciano;
    }

    /// <summary>
    /// Realza la imagen sumándole el resultado del Laplaciano.
    /// Realce = Imagen Original + Bordes (⌒‿⌒)
    /// </summary>
    public Bitmap AplicarRealceConLaplaciano(Bitmap imagen)
    {
        // Realzar sumando la imagen original + el resultado del Laplaciano
        Add addFilter = new Add(imagen);
        Bitmap imagenRealzada = addFilter.Apply(AplicarLaplaciano(imagen));

        return imagenRealzada;
    }

    // B) FILTRO HIGH-BOOST

    /// <summary>
    /// Aplica el filtro High-Boost para resaltar detalles finos y mejorar nitidez.
    /// Multiplica detalles por (A-1) y los suma a la imagen~ ☆
    /// </summary>
    public Bitmap AplicarHighBoost(Bitmap imagenGris, double factorA)
    {
        // Forzar formato 24 bits para seguridad
        Bitmap imagenGris24bpp = imagenGris.Clone(new Rectangle(0, 0, imagenGris.Width, imagenGris.Height), PixelFormat.Format24bppRgb);

        // Suavizar la imagen usando desenfoque Gaussiano
        Blur blurFilter = new Blur();
        Bitmap imagenSuavizada = blurFilter.Apply(imagenGris24bpp);

        // Asegurarse que la imagen suavizada también sea 24bpp
        Bitmap imagenSuavizada24bpp = imagenSuavizada.Clone(new Rectangle(0, 0, imagenSuavizada.Width, imagenSuavizada.Height), PixelFormat.Format24bppRgb);

        // Crear máscara de detalles (original - suavizada)
        Subtract subtractFilter = new Subtract(imagenGris24bpp);
        Bitmap mascaraDetalles = subtractFilter.Apply(imagenSuavizada24bpp);

        // Multiplicar detalles por (A-1) para amplificar
        Bitmap mascaraAmplificada = MultiplicarImagenEscalar(mascaraDetalles, factorA - 1);

        // Asegurar que la máscara también esté en 24bpp
        Bitmap mascaraAmplificada24bpp = mascaraAmplificada.Clone(new Rectangle(0, 0, mascaraAmplificada.Width, mascaraAmplificada.Height), PixelFormat.Format24bppRgb);

        // Sumar la máscara amplificada a la imagen original
        Add addFilter = new Add(imagenGris24bpp);
        Bitmap imagenHighBoost = addFilter.Apply(mascaraAmplificada24bpp);

        return imagenHighBoost;
    }

    /// <summary>
    /// Multiplica cada pixel de la imagen por un valor escalar.
    /// Se asegura de mantener los valores dentro de [0, 255].
    /// </summary>
    public Bitmap MultiplicarImagenEscalar(Bitmap imagen, double escalar)
    {
        Bitmap nuevaImagen = new Bitmap(imagen.Width, imagen.Height);

        for (int y = 0; y < imagen.Height; y++)
        {
            for (int x = 0; x < imagen.Width; x++)
            {
                Color pixel = imagen.GetPixel(x, y);
                int valor = (int)(pixel.R * escalar);

                // Limitar valores entre 0 y 255 para evitar desbordes
                valor = Math.Max(0, Math.Min(255, valor));

                nuevaImagen.SetPixel(x, y, Color.FromArgb(valor, valor, valor));
            }
        }

        return nuevaImagen;
    }
    /// <summary>
    /// Aplica el filtro de Difusión Anisotrópica a color utilizando el modelo de Perona-Malik.
    /// Este proceso suaviza las regiones internas de la imagen sin borrar los bordes importantes.
    /// Es ideal para reducir ruido sin perder nitidez. （人*´∀｀）
    /// </summary>
    public Bitmap AplicarDifusionAnisotropica(Bitmap imagen, int iteraciones, double lambda = 0.25, double k = 15.0)
    {
        int width = imagen.Width;
        int height = imagen.Height;

        Bitmap resultado = new Bitmap(width, height, PixelFormat.Format24bppRgb);

        BitmapData dataOriginal = imagen.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
        BitmapData dataResultado = resultado.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

        int stride = dataOriginal.Stride;
        unsafe
        {
            byte* ptrOriginal = (byte*)dataOriginal.Scan0;
            byte* ptrResultado = (byte*)dataResultado.Scan0;

            // Matrices para los 3 canales
            double[,] canalR = new double[width, height];
            double[,] canalG = new double[width, height];
            double[,] canalB = new double[width, height];

            // Cargar los datos en matrices
            for (int y = 0; y < height; y++)
            {
                byte* row = ptrOriginal + (y * stride);
                for (int x = 0; x < width; x++)
                {
                    int idx = x * 3;
                    canalB[x, y] = row[idx];     // Blue
                    canalG[x, y] = row[idx + 1]; // Green
                    canalR[x, y] = row[idx + 2]; // Red
                }
            }

            // AplicarErosion cada canal por separado
            canalR = DifusionAnisotropica(canalR, iteraciones, lambda, k);
            canalG = DifusionAnisotropica(canalG, iteraciones, lambda, k);
            canalB = DifusionAnisotropica(canalB, iteraciones, lambda, k);

            // Escribir los datos de nuevo
            for (int y = 0; y < height; y++)
            {
                byte* row = ptrResultado + (y * stride);
                for (int x = 0; x < width; x++)
                {
                    int idx = x * 3;
                    row[idx] = (byte)Math.Max(0, Math.Min(255, (int)canalB[x, y]));
                    row[idx + 1] = (byte)Math.Max(0, Math.Min(255, (int)canalG[x, y]));
                    row[idx + 2] = (byte)Math.Max(0, Math.Min(255, (int)canalR[x, y]));

                }
            }
        }

        imagen.UnlockBits(dataOriginal);
        resultado.UnlockBits(dataResultado);

        return resultado;
    }


    /// <summary>
    /// Aplica la difusión anisotrópica (modelo Perona-Malik) a un solo canal de la imagen.
    /// Reduce el ruido preservando los bordes importantes~ UwU
    /// </summary>
    private double[,] DifusionAnisotropica(double[,] datos, int iteraciones, double lambda, double k)
    {
        int width = datos.GetLength(0);
        int height = datos.GetLength(1);

        // Realizamos el proceso varias veces para que el efecto sea acumulativo
        for (int i = 0; i < iteraciones; i++)
        {
            // Creamos una copia del canal original para almacenar los nuevos valores
            double[,] nueva = (double[,])datos.Clone();

            // Recorremos cada píxel (evitamos los bordes por seguridad)
            for (int y = 1; y < height - 1; y++)
            {
                for (int x = 1; x < width - 1; x++)
                {
                    // Calculamos diferencias con los vecinos (norte, sur, este, oeste)
                    double norte = datos[x, y - 1] - datos[x, y];
                    double sur = datos[x, y + 1] - datos[x, y];
                    double este = datos[x + 1, y] - datos[x, y];
                    double oeste = datos[x - 1, y] - datos[x, y];

                    // Calculamos los coeficientes de difusión usando una función exponencial
                    // Esta parte controla cuánto dejamos pasar de cada dirección.
                    double cN = Math.Exp(-Math.Pow(norte / k, 2));
                    double cS = Math.Exp(-Math.Pow(sur / k, 2));
                    double cE = Math.Exp(-Math.Pow(este / k, 2));
                    double cW = Math.Exp(-Math.Pow(oeste / k, 2));

                    // Actualizamos el valor del píxel usando los coeficientes
                    // Este es el corazón del algoritmo ∑(≧ω≦*)
                    nueva[x, y] = datos[x, y] + lambda * (
                        cN * norte +
                        cS * sur +
                        cE * este +
                        cW * oeste
                    );
                }
            }

            // La nueva matriz procesada se convierte en la base para la siguiente iteración
            datos = nueva;
        }

        return datos;
    }


}
