using modificacion_de_imagen.clases;
using modificacion_de_imagen.MenusFiltros;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace modificacion_de_imagen
{
    public partial class MenuEditor : Form
    {
        private ImagenManager imagenManager;
        private AplicadorFiltros filtroAplicador;
        private MapeoEtiquetado mapeoEtiquetado = new MapeoEtiquetado();

        private readonly AjustesImagen ajustesImagen = new AjustesImagen();
        private readonly FiltrosColor filtrosColor = new FiltrosColor();
        private readonly FiltrosGrisesYUmbral filtrosGrisesYUmbral = new FiltrosGrisesYUmbral();
        private readonly FiltrosPosterizadoSolarizado filtrosPosterizadoSolarizado = new FiltrosPosterizadoSolarizado();
        private readonly FiltrosBordesSuavizado filtrosBordesSuavizado = new FiltrosBordesSuavizado();
        private readonly FiltrodConFourier filtrosConFourier = new FiltrodConFourier();
        private readonly FiltrosPracticos2 filtrosPractico2 = new FiltrosPracticos2();
        private readonly filtrosArtisticos filtrosArtisticos = new filtrosArtisticos();
        private readonly filtrosDetecionBordes filtrosDetecionBordes = new filtrosDetecionBordes();

        public MenuEditor()
        {
            InitializeComponent();
            progressBar1.Visible = false;
            imagenManager = new ImagenManager();
            filtroAplicador = new AplicadorFiltros(progressBar1);
        }

        private async void AplicarFiltro(Func<Bitmap, Bitmap> filtro)
        {
            if (pictureBox1.Image is Bitmap imagen)
            {
                pictureBox1.Image = await filtroAplicador.AplicarFiltroAsync(imagen, filtro);
            }
        }

        private async void AplicarFiltroConDialog(Func<Bitmap, Form> crearDialogo)
        {
            if (pictureBox1.Image is Bitmap imagen)
            {
                pictureBox1.Image = await filtroAplicador.AplicarFiltroConDialogAsync(imagen, crearDialogo);
            }
        }

        private void MostrarInformacionImagen(Bitmap imagen, string nombre)
        {
            if (lbInformacion == null) return;

            lbInformacion.Text = $"Nombre: {nombre} | Dimensiones: {imagen.Width}x{imagen.Height}px | " +
                                 $"Formato: {imagen.PixelFormat}";
        }

        // --- Menú eventos ---
        private void cargarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = imagenManager.CargarImagen(out string nombre);
            if (pictureBox1.Image != null)
            {
                MostrarInformacionImagen((Bitmap)pictureBox1.Image, nombre);
                InageToolStripMenuItem.Enabled = true;
                guardarToolStripMenuItem.Enabled = true;
                edicionToolStripMenuItem.Enabled = true;
            }
        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                imagenManager.GuardarImagen(pictureBox1.Image, imagenManager.NombreArchivoCargado);
            }
        }

        private void revertirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = imagenManager.RevertirImagen();
        }

        // --- Filtros ---
        private void escalaDeGrisesToolStripMenuItem_Click(object sender, EventArgs e) => AplicarFiltro(filtrosGrisesYUmbral.AplicarEscalaGrises);
        private void umbralToolStripMenuItem_Click(object sender, EventArgs e) => AplicarFiltroConDialog(img => new UmbralDialog(img));
        private void posterizadoToolStripMenuItem_Click(object sender, EventArgs e) => AplicarFiltroConDialog(img => new PosterizeDialog(img));
        private void solarizadoToolStripMenuItem_Click(object sender, EventArgs e) => AplicarFiltroConDialog(img => new SolarizadoDialog(img));
        private void umbralOtsuToolStripMenuItem_Click(object sender, EventArgs e) => AplicarFiltro(filtrosGrisesYUmbral.AplicarUmbralOtsu);
        private void simplePosterizationToolStripMenuItem_Click(object sender, EventArgs e) => AplicarFiltro(filtrosPosterizadoSolarizado.AplicarSamplePosterizado);
        private void medianaToolStripMenuItem_Click(object sender, EventArgs e) => AplicarFiltro(filtrosPosterizadoSolarizado.AplicarMedia);

        // Filtros de color
        private void falsoColorToolStripMenuItem_Click(object sender, EventArgs e) => AplicarFiltro(filtrosColor.AplicarFalseColor);
        private void invertirToolStripMenuItem_Click(object sender, EventArgs e) => AplicarFiltro(filtrosColor.AplicarInvertir);

        // Ajustes
        private void brillocontrasteToolStripMenuItem_Click(object sender, EventArgs e) => AplicarFiltroConDialog(img => new Brillo_contrasteDialog(img));
        private void SaturacionToolStripMenuItem_Click(object sender, EventArgs e) => AplicarFiltroConDialog(img => new SaturacionDialog(img));
        private void histogramaEquatizacionToolStripMenuItem_Click(object sender, EventArgs e) => AplicarFiltro(ajustesImagen.AplicarHistogramaEcualizacion);

        // Bordes y suavizado
        private void realceBordesToolStripMenuItem_Click(object sender, EventArgs e) => AplicarFiltro(filtrosBordesSuavizado.AplicarRealceBordes);
        private void pasoAltoToolStripMenuItem_Click(object sender, EventArgs e) => AplicarFiltroConDialog(img => new PasoAltoDialog(img));
        private void DesenfoqueBordesToolStripMenuItem_Click(object sender, EventArgs e) => AplicarFiltro(filtrosBordesSuavizado.AplicarDesenfoqueBordes);
        private void pasoBajoToolStripMenuItem_Click(object sender, EventArgs e) => AplicarFiltroConDialog(img => new PasoBajoDialog(img));

        // Fourier
        private void FFT_ToolStripMenuItem_Click(object sender, EventArgs e) => AplicarFiltro(filtrosConFourier.AplicarTransformada);
        private void FFT_inversa_ToolStripMenuItem_Click(object sender, EventArgs e) => pictureBox1.Image = filtrosConFourier.AplicarTransformadaInversa();
        private void convoluciónEnFrecuenciaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null) return;
            var original = new Bitmap(pictureBox1.Image);
            float[,] kernel = { { 0, -1, 0 }, { -1, 5, -1 }, { 0, -1, 0 } };
            var kernelImg = filtrosConFourier.CrearImagenDesdeKernel(kernel, original.Width, original.Height);
            pictureBox1.Image = filtrosConFourier.ConvolucionFFT(original, kernelImg);
        }
        private void convoluciónDirectaToolStripMenuItem_Click(object sender, EventArgs e) => AplicarFiltro(filtrosConFourier.ConvolucionDirecta);
        private void compararConvolucionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null) return;
            Bitmap original = new Bitmap(pictureBox1.Image);
            pictureBox1.Image = filtrosConFourier.CompararConvoluciones(original);
        }

        // Otros filtros
        private void tamañoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null) return;
            pictureBox1.Image = ajustesImagen.RedimensionarImagen(new Bitmap(pictureBox1.Image), 512, 512);
        }

        private void highboostToolStripMenuItem_Click(object sender, EventArgs e) => AplicarFiltroConDialog(img => new HightBostDialog(img));
        private void difusionAsinotropicaToolStripMenuItem_Click(object sender, EventArgs e) => AplicarFiltroConDialog(img => new difusionAsinotropica(img));
        private void realceLaplacianoToolStripMenuItem_Click(object sender, EventArgs e) => AplicarFiltro(filtrosPractico2.AplicarRealceConLaplaciano);

        // Artísticos
        private void pinturaOleoToolStripMenuItem_Click(object sender, EventArgs e) => AplicarFiltro(filtrosArtisticos.PinturaOleo);
        private void pixeladoToolStripMenuItem_Click(object sender, EventArgs e) => AplicarFiltro(filtrosArtisticos.Pixelar);

        // Detección de bordes
        private void robertToolStripMenuItem_Click(object sender, EventArgs e) => AplicarFiltro(filtrosDetecionBordes.AplicarRoberts);
        private void prewitToolStripMenuItem_Click(object sender, EventArgs e) => AplicarFiltro(filtrosDetecionBordes.AplicarDifference);
        private void sobelToolStripMenuItem_Click(object sender, EventArgs e) => AplicarFiltro(filtrosDetecionBordes.AplicarSobel);
        private void laplacianoToolStripMenuItem_Click(object sender, EventArgs e) => AplicarFiltro(filtrosDetecionBordes.AplicarLaplaciano);
        private void laplacianoGaussToolStripMenuItem_Click(object sender, EventArgs e) => AplicarFiltro(filtrosDetecionBordes.AplicarLaplacianoGauss);
        private void cannyToolStripMenuItem_Click(object sender, EventArgs e) => AplicarFiltro(filtrosDetecionBordes.AplicarCanny);

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = mapeoEtiquetado.ColorearObjetosEncontrados(new Bitmap(pictureBox1.Image));
            lbInformacion.Text = "se han contaron con 4 conectividad " + mapeoEtiquetado.ContarObjetos(new Bitmap(pictureBox1.Image), 4);
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            lbInformacion.Text = "se han contaron con 8 conectividad " + mapeoEtiquetado.ContarObjetos(new Bitmap(pictureBox1.Image), 8);
            pictureBox1.Image = mapeoEtiquetado.ColorearObjetosEncontrados(new Bitmap(pictureBox1.Image));
            
        }

        private void euclidianToolStripMenuItem_Click(object sender, EventArgs e)
        => AplicarFiltroConDialog(img => new FormEuclidiana(img));

        private void ManhattanToolStripMenuItem_Click(object sender, EventArgs e)
        => AplicarFiltroConDialog(img => new FormManhattan(img));

        private void chebyshevToolStripMenuItem_Click(object sender, EventArgs e)
       => AplicarFiltroConDialog(img => new FormChebyshev(img));

        private void gammaCorrectionToolStripMenuItem_Click(object sender, EventArgs e)
       => AplicarFiltro(ajustesImagen.Aplicargamma);
    }
}
