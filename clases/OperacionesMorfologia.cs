using System;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;

namespace modificacion_de_imagen.clases
{
    public enum TipoOperacionMorfologica
    {
        Erosion,
        Apertura,
        Cierre,
        Bordes
    }

    public class OperacionesMorfologia
    {
        /// <summary>
        /// Aplica operación morfológica usando EmguCV y un kernel cuadrado de tamaño específico.
        /// </summary>
        public Bitmap AplicarOperacionMorfologica(Bitmap bitmapOriginal, int umbral, TipoOperacionMorfologica operacion, int kernelSize = 3)
        {
            // Convertir a formato de EmguCV
            Image<Bgr, byte> imgColor = bitmapOriginal.ToImage<Bgr, byte>();
            Image<Gray, byte> imgGray = imgColor.Convert<Gray, byte>();

            // Aplicar umbral
            Image<Gray, byte> imgBinaria = imgGray.ThresholdBinary(new Gray(umbral), new Gray(255));

            // Crear kernel
            Mat kernel = CvInvoke.GetStructuringElement(ElementShape.Rectangle, new Size(kernelSize, kernelSize), new Point(-1, -1));

            Image<Gray, byte> resultado = imgBinaria.CopyBlank();

            switch (operacion)
            {
                case TipoOperacionMorfologica.Erosion:
                    CvInvoke.Erode(imgBinaria, resultado, kernel, new Point(-1, -1), 1, BorderType.Default, default(MCvScalar));
                    break;

                case TipoOperacionMorfologica.Apertura:
                    CvInvoke.MorphologyEx(imgBinaria, resultado, MorphOp.Open, kernel, new Point(-1, -1), 1, BorderType.Default, default(MCvScalar));
                    break;

                case TipoOperacionMorfologica.Cierre:
                    CvInvoke.MorphologyEx(imgBinaria, resultado, MorphOp.Close, kernel, new Point(-1, -1), 1, BorderType.Default, default(MCvScalar));
                    break;

                case TipoOperacionMorfologica.Bordes:
                    Image<Gray, byte> erosionada = imgBinaria.CopyBlank();
                    CvInvoke.Erode(imgBinaria, erosionada, kernel, new Point(-1, -1), 1, BorderType.Default, default(MCvScalar));
                    resultado = imgBinaria - erosionada;
                    break;

                default:
                    throw new ArgumentException("Operación no soportada.");
            }

            return resultado.ToBitmap();
        }

    }
}


