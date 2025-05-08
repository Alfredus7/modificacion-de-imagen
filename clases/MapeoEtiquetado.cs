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
        // (｡♥‿♥｡) Método para contar objetos con conectividad-4 o conectividad-8
        // Este método permite contar los objetos en una imagen binaria usando dos tipos de conectividad:
        // 1. Conectividad-4: los objetos se consideran conectados solo si comparten un borde horizontal o vertical.
        // 2. Conectividad-8: los objetos se consideran conectados si comparten un borde, o un vértice (diagonal).
        public int ContarObjetos(Bitmap imagen, int conectividad)
        {
            var imagenBinaria = BinarizarImagen(imagen);

            // Llamada al método apropiado dependiendo de la conectividad seleccionada
            return conectividad == 4
                ? ContarObjetosConectividad4(imagenBinaria)  // Método de conectividad-4
                : ContarObjetosConectividad8(imagenBinaria); // Método de conectividad-8
        }

        // (✿◠‿◠) Método mágico para binarizar la imagen
        // Convierte la imagen en escala de grises y luego la binariza usando un umbral.
        // El umbral se establece en 1 para hacer la imagen binaria (blanco y negro).
        private Bitmap BinarizarImagen(Bitmap imagen)
        {
            var grayscale = new Grayscale(0.2125, 0.7154, 0.0721); // Conversión a escala de grises
            var imagenGris = grayscale.Apply(imagen);
            var binarizador = new Threshold(1); // Binarización
            return binarizador.Apply(imagenGris);
        }

        // (╹◡╹) Conteo con conectividad-4 usando BFS (Breadth-First Search)
        // En la conectividad-4, un píxel está conectado a otros píxeles si están en una de las 4 direcciones cardinales (arriba, abajo, izquierda, derecha).
        // Este método usa un algoritmo BFS para explorar todos los píxeles conectados a un píxel inicial.
        private int ContarObjetosConectividad4(Bitmap imagen)
        {
            int width = imagen.Width, height = imagen.Height;
            bool[,] visitado = new bool[width, height]; // Matriz para marcar píxeles visitados
            int count = 0; // Contador de objetos encontrados

            // Definimos los movimientos posibles para conectividad-4
            int[] dx = { -1, 1, 0, 0 }; // Movimientos horizontales
            int[] dy = { 0, 0, -1, 1 }; // Movimientos verticales

            BitmapData data = imagen.LockBits(new Rectangle(0, 0, width, height),
                                              ImageLockMode.ReadOnly,
                                              PixelFormat.Format8bppIndexed);

            unsafe
            {
                byte* ptr = (byte*)data.Scan0;
                int stride = data.Stride;

                // Función BFS para explorar objetos conectados
                void BFS(int x, int y)
                {
                    Queue<Point> cola = new Queue<Point>();
                    cola.Enqueue(new Point(x, y)); // Añadimos el punto inicial a la cola
                    visitado[x, y] = true; // Marcamos el punto como visitado

                    while (cola.Count > 0)
                    {
                        Point p = cola.Dequeue();

                        // Iteramos sobre los 4 posibles movimientos (conectividad-4)
                        for (int i = 0; i < 4; i++)
                        {
                            int nx = p.X + dx[i], ny = p.Y + dy[i];
                            // Verificamos si el nuevo punto está dentro de los límites de la imagen y no ha sido visitado
                            if (nx >= 0 && ny >= 0 && nx < width && ny < height &&
                                !visitado[nx, ny] && ptr[ny * stride + nx] == 255) // El valor 255 indica píxel blanco (objeto)
                            {
                                cola.Enqueue(new Point(nx, ny));
                                visitado[nx, ny] = true; // Marcamos el píxel como visitado
                            }
                        }
                    }
                }

                // Recorremos toda la imagen buscando objetos
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        if (ptr[y * stride + x] == 255 && !visitado[x, y]) // Si es un píxel blanco no visitado
                        {
                            BFS(x, y); // Llamamos a BFS para contar este objeto
                            count++; // Incrementamos el contador de objetos
                        }
                    }
                }
            }

            imagen.UnlockBits(data); // Desbloqueamos la imagen después de procesarla
            return count;
        }

        // (◕‿◕✿) Conteo de objetos con conectividad-8 usando Accord
        // En la conectividad-8, un píxel está conectado a otros píxeles si están en cualquiera de las 8 direcciones posibles.
        // Esto incluye las 4 direcciones cardinales y las 4 diagonales.
        private int ContarObjetosConectividad8(Bitmap imagen)
        {
            var blobCounter = new BlobCounter
            {
                FilterBlobs = true,
                MinWidth = 1,
                MinHeight = 1,
                ObjectsOrder = ObjectsOrder.None
            };

            blobCounter.ProcessImage(imagen); // Procesamos la imagen para contar objetos
            return blobCounter.GetObjectsInformation().Length; // Retorna la cantidad de objetos encontrados
        }

        // (≧◡≦) Método superkawaii para colorear los objetos encontrados ✧*:･ﾟ✧
        // Este método asigna un color único a cada objeto encontrado, coloreándolos sobre la imagen original.
        public Bitmap ColorearObjetosEncontrados(Bitmap original)
        {
            var imagenBinaria = BinarizarImagen(original);
            var blobCounter = new BlobCounter
            {
                FilterBlobs = true,
                MinWidth = 1,
                MinHeight = 1,
                ObjectsOrder = ObjectsOrder.None
            };
            blobCounter.ProcessImage(imagenBinaria);
            Blob[] blobs = blobCounter.GetObjectsInformation();

            Bitmap resultado = new Bitmap(original.Width, original.Height, PixelFormat.Format24bppRgb);

            using (Graphics g = Graphics.FromImage(resultado))
                g.Clear(Color.Black); // Fondo negro para resaltar los objetos

            Color[] colores = {
                Color.Red, Color.Lime, Color.Blue, Color.Yellow,
                Color.Cyan, Color.Magenta, Color.Orange, Color.Pink,
                Color.Purple, Color.Brown, Color.Teal, Color.Olive
            };
            Random rand = new Random();

            foreach (Blob blob in blobs)
            {
                Color color = Array.IndexOf(blobs, blob) < colores.Length ?
                    colores[Array.IndexOf(blobs, blob)] :
                    Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256)); // Asignamos un color

                // Extraemos la imagen del blob y copiamos los píxeles al resultado
                blobCounter.ExtractBlobsImage(imagenBinaria, blob, false);

                for (int y = 0; y < blob.Rectangle.Height; y++)
                {
                    for (int x = 0; x < blob.Rectangle.Width; x++)
                    {
                        if (blob.Image != null && blob.Image.GetPixel(x, y).R > 0)
                        {
                            resultado.SetPixel(blob.Rectangle.X + x, blob.Rectangle.Y + y, color);
                        }
                    }
                }
            }

            return resultado;
        }
    }
}
