using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using Accord.Imaging;
using Accord.Imaging.Filters;

namespace modificacion_de_imagen.clases
{
    public class MapeoEtiquetado
    {

        // Método para contar objetos con conectividad-4 o conectividad-8
        public int ContarObjetos(Bitmap imagen, int conectividad)
        {
            // Convertimos la imagen a blanco y negro para trabajar con binario
            Grayscale grayscale = new Grayscale(0.2125, 0.7154, 0.0721);
            Bitmap imagenGris = grayscale.Apply(imagen);
            Threshold binarizador = new Threshold(1);
            Bitmap imagenBinaria = binarizador.Apply(imagenGris);

            // Dependiendo de la conectividad, usaremos la búsqueda personalizada
            if (conectividad == 4)
                return ContarObjetosConectividad4(imagenBinaria);
            else
                return ContarObjetosConectividad8(imagenBinaria);
        }

        // Conteo de objetos usando conectividad-4 (BFS)
        private int ContarObjetosConectividad4(Bitmap imagen)
        {
            int width = imagen.Width;
            int height = imagen.Height;
            bool[,] visitado = new bool[width, height];
            int count = 0;

            // Direcciones de conectividad-4: arriba, abajo, izquierda, derecha
            int[] dx = { -1, 1, 0, 0 };
            int[] dy = { 0, 0, -1, 1 };

            // Función para realizar una búsqueda BFS
            void BFS(int x, int y)
            {
                Queue<Point> cola = new Queue<Point>();
                cola.Enqueue(new Point(x, y));
                visitado[x, y] = true;

                while (cola.Count > 0)
                {
                    var punto = cola.Dequeue();

                    for (int i = 0; i < 4; i++)  // Recorremos las 4 direcciones
                    {
                        int nuevoX = punto.X + dx[i];
                        int nuevoY = punto.Y + dy[i];

                        // Verificar que esté dentro de los límites de la imagen y no haya sido visitado
                        if (nuevoX >= 0 && nuevoY >= 0 && nuevoX < width && nuevoY < height &&
                            !visitado[nuevoX, nuevoY] && imagen.GetPixel(nuevoX, nuevoY).R == 255)  // 255 es blanco
                        {
                            cola.Enqueue(new Point(nuevoX, nuevoY));
                            visitado[nuevoX, nuevoY] = true;
                        }
                    }
                }
            }

            // Recorremos cada píxel de la imagen
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (imagen.GetPixel(x, y).R == 255 && !visitado[x, y])  // Si es blanco y no ha sido visitado
                    {
                        BFS(x, y);
                        count++;
                    }
                }
            }

            return count;
        }

        // Conteo de objetos usando conectividad-8 (usando Accord)
        private int ContarObjetosConectividad8(Bitmap imagen)
        {
            // Procesamos la imagen con BlobCounter para conectividad-8
            BlobCounter blobCounter = new BlobCounter
            {
                FilterBlobs = true,
                MinWidth = 1,
                MinHeight = 1,
                ObjectsOrder = ObjectsOrder.None
            };

            blobCounter.ProcessImage(imagen);
            Blob[] blobs = blobCounter.GetObjectsInformation();

            return blobs.Length;
        }

        public Bitmap ColorearObjetosEncontrados(Bitmap original)
        {
            // First convert to grayscale and threshold if needed
            Grayscale grayscale = new Grayscale(0.2125, 0.7154, 0.0721);
            Bitmap imagenGris = grayscale.Apply(original);
            Threshold binarizador = new Threshold(1);
            Bitmap imagenBinaria = binarizador.Apply(imagenGris);

            BlobCounter blobCounter = new BlobCounter
            {
                FilterBlobs = true,
                MinWidth = 1,
                MinHeight = 1,
                ObjectsOrder = ObjectsOrder.None
            };

            // Process the image first!
            blobCounter.ProcessImage(imagenBinaria);

            // Crear bitmap de resultado con el mismo formato que el original
            Bitmap resultado = new Bitmap(original.Width, original.Height, original.PixelFormat);

            using (Graphics g = Graphics.FromImage(resultado))
            {
                g.Clear(Color.Black); // Fondo negro

                // Obtener información de cada blob
                Blob[] blobs = blobCounter.GetObjectsInformation();

                // Paleta de colores predefinida
                Color[] colores = new Color[]
                {
            Color.Red, Color.Lime, Color.Blue, Color.Yellow,
            Color.Cyan, Color.Magenta, Color.Orange, Color.Pink,
            Color.Purple, Color.Brown, Color.Teal, Color.Olive
                };

                Random rand = new Random();

                foreach (Blob blob in blobs)
                {
                    Color color = Array.IndexOf(blobs, blob) < colores.Length ?
                        colores[Array.IndexOf(blobs, blob)] :
                        Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256));

                    // Extraer la imagen del blob
                    blobCounter.ExtractBlobsImage(imagenBinaria, blob, false);

                    // Copiar los píxeles del blob al resultado
                    for (int y = 0; y < blob.Rectangle.Height; y++)
                    {
                        for (int x = 0; x < blob.Rectangle.Width; x++)
                        {
                            // Verificar si el píxel pertenece al blob
                            if (blob.Image != null && blob.Image.GetPixel(x, y).R > 0)
                            {
                                resultado.SetPixel(blob.Rectangle.X + x, blob.Rectangle.Y + y, color);
                            }
                        }
                    }
                }
            }

            return resultado;
        }

    }
}
