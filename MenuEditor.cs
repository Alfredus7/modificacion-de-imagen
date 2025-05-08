using modificacion_de_imagen.MenusFiltros;
using modificacion_de_imagen.clases;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace modificacion_de_imagen
{
    public partial class MenuEditor : Form
    {
        private string nombreArchivoCargado;
        private Bitmap imagenOriginal;

        // FiltrosPracticos2
        private readonly AjustesImagen ajustesImagen = new AjustesImagen();
        private readonly FiltrosBordesSuavizado filtrosBordesSuavizado = new FiltrosBordesSuavizado();
        private readonly FiltrosColor filtrosColor = new FiltrosColor();
        private readonly FiltrosGrisesYUmbral filtrosGrisesYUmbral = new FiltrosGrisesYUmbral();
        private readonly FiltrosPosterizadoSolarizado filtrosPosterizadoSolarizado = new FiltrosPosterizadoSolarizado();
        private readonly TransformadasFourier filtrosFourierHelper = new TransformadasFourier();
        private readonly FiltrosPracticos2 filtrosPractico2 = new FiltrosPracticos2();
        private readonly filtrosArtisticos filtrosArtisticos = new filtrosArtisticos();
        private readonly filtrosDetecionBordes filtrosDetecionBordes = new filtrosDetecionBordes();
        public MenuEditor()
        {
            InitializeComponent();
            progressBar1.Visible = false; // Asegúrate de tener un ProgressBar en el formulario
        }

        #region --- Carga / Guardado / Reversión ---

        private void CargarImagen()
        {
            OpenFileDialog ofd = new OpenFileDialog { Filter = @"Archivos de imagen|*.jpg;*.png;*.jpeg" };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                nombreArchivoCargado = System.IO.Path.GetFileNameWithoutExtension(ofd.FileName);
                if (ofd.FileName != null) imagenOriginal = new Bitmap(ofd.FileName);

                pictureBox1.Image = ajustesImagen.ConvertirAFormatoCompatible(new Bitmap(imagenOriginal));

                MostrarInformacionImagen(imagenOriginal, nombreArchivoCargado);

                InageToolStripMenuItem.Enabled = true;
                guardarToolStripMenuItem.Enabled = true;
                edicionToolStripMenuItem.Enabled = true;
            }
        }

        private void GuardarImagen()
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("No hay ninguna imagen para guardar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "Imagen JPEG|*.jpg|Imagen PNG|*.png",
                Title = "Guardar imagen",
                FileName = nombreArchivoCargado + " Copia"
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string ext = System.IO.Path.GetExtension(sfd.FileName).ToLower();
                    var formato = ext == ".jpg" ? System.Drawing.Imaging.ImageFormat.Jpeg : System.Drawing.Imaging.ImageFormat.Png;
                    pictureBox1.Image.Save(sfd.FileName, formato);
                    MessageBox.Show("Imagen guardada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al guardar la imagen: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void RevertirImagen()
        {
            if (imagenOriginal != null)
            {
                pictureBox1.Image = new Bitmap(imagenOriginal);
                MostrarInformacionImagen(imagenOriginal, nombreArchivoCargado);
            }
        }

        private void MostrarInformacionImagen(Bitmap imagen, string nombre)
        {
            if (lbInformacion == null) return;

            lbInformacion.Text = $"Nombre: {nombre} | Dimensiones: {imagen.Width}x{imagen.Height}px | " +
                                 $"Formato: {imagen.PixelFormat} | DPI: {imagen.HorizontalResolution} x {imagen.VerticalResolution}";
        }

        #endregion

        #region --- Filtros de Imagen ---

        private async void AplicarFiltro(Func<Bitmap, Bitmap> filtro)
        {
            if (pictureBox1.Image != null)
            {
                progressBar1.Visible = true;
                progressBar1.Style = ProgressBarStyle.Marquee;

                try
                {
                    Bitmap imagenProcesada = await Task.Run(() => filtro(new Bitmap(pictureBox1.Image)));
                    pictureBox1.Image = imagenProcesada;
                }
                finally
                {
                    progressBar1.Visible = false;
                }
            }
        }

        private async void AplicarFiltroConDialog(Func<Bitmap, Form> crearDialogo)
        {
            if (pictureBox1.Image != null)
            {
                progressBar1.Visible = true;
                progressBar1.Style = ProgressBarStyle.Marquee;

                try
                {
                    var dialog = crearDialogo(new Bitmap(pictureBox1.Image));
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        pictureBox1.Image = ((dynamic)dialog).ImageToProcess;
                    }
                }
                finally
                {
                    progressBar1.Visible = false;
                }
            }
        }

        #endregion

        #region --- Eventos Menú: Carga / Guardado ---

        private void cargarToolStripMenuItem_Click(object sender, EventArgs e) => CargarImagen();
        private void guardarToolStripMenuItem_Click(object sender, EventArgs e) => GuardarImagen();
        private void revertirToolStripMenuItem_Click(object sender, EventArgs e) => RevertirImagen();

        #endregion

        #region --- Filtros Básicos ---

        private void escalaDeGrisesToolStripMenuItem_Click(object sender, EventArgs e) =>
            AplicarFiltro(filtrosGrisesYUmbral.AplicarEscalaGrises);

        private void umbralToolStripMenuItem_Click(object sender, EventArgs e) =>
            AplicarFiltroConDialog(img => new UmbralDialog(img));

        private void posterizadoToolStripMenuItem_Click(object sender, EventArgs e) =>
            AplicarFiltroConDialog(img => new PosterizeDialog(img));

        private void solarizadoToolStripMenuItem_Click(object sender, EventArgs e) =>
            AplicarFiltroConDialog(img => new SolarizadoDialog(img));

        private void umbralOtsuToolStripMenuItem_Click(object sender, EventArgs e) =>
            AplicarFiltro(filtrosGrisesYUmbral.AplicarUmbralOtsu);

        private void simplePosterizationToolStripMenuItem_Click(object sender, EventArgs e) =>
            AplicarFiltro(filtrosPosterizadoSolarizado.AplicarSamplePosterizado);

        private void medianaToolStripMenuItem_Click(object sender, EventArgs e) =>
            AplicarFiltro(filtrosPosterizadoSolarizado.AplicarMedia);

        #endregion

        #region --- Filtros de Color ---

        private void falsoColorToolStripMenuItem_Click(object sender, EventArgs e) =>
            AplicarFiltro(filtrosColor.AplicarFalseColor);

        private void invertirToolStripMenuItem_Click(object sender, EventArgs e) =>
            AplicarFiltro(filtrosColor.AplicarInvertir);

        #endregion

        #region --- Ajustes de Imagen ---

        private void brillocontrasteToolStripMenuItem_Click(object sender, EventArgs e) =>
            AplicarFiltroConDialog(img => new Brillo_contrasteDialog(img));

        private void SaturacionToolStripMenuItem_Click(object sender, EventArgs e) =>
            AplicarFiltroConDialog(img => new SaturacionDialog(img));

        private void histogramaEquatizacionToolStripMenuItem_Click(object sender, EventArgs e) =>
            AplicarFiltro(ajustesImagen.AplicarHistogramaEcualizacion);

        #endregion

        #region --- Realce y desenfoque ---

        private void realceBordesToolStripMenuItem_Click(object sender, EventArgs e) =>
            AplicarFiltro(filtrosBordesSuavizado.AplicarRealceBordes);

        private void pasoAltoToolStripMenuItem_Click(object sender, EventArgs e) =>
            AplicarFiltroConDialog(img => new PasoAltoDialog(img));

        private void DesenfoqueBordesToolStripMenuItem_Click(object sender, EventArgs e) =>
            AplicarFiltro(filtrosBordesSuavizado.AplicarDesenfoqueBordes);

        private void pasoBajoToolStripMenuItem_Click(object sender, EventArgs e) =>
            AplicarFiltroConDialog(img => new PasoBajoDialog(img));


        #endregion

        #region --- Transformadas de Fourier ---

        private void FFT_ToolStripMenuItem_Click(object sender, EventArgs e) =>
            AplicarFiltro(filtrosFourierHelper.AplicarTransformada);

        private void FFT_inversa_ToolStripMenuItem_Click(object sender, EventArgs e) =>
            pictureBox1.Image = filtrosFourierHelper.AplicarTransformadaInversa();

        private void convoluciónEnFrecuenciaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null) return;

            Bitmap original = new Bitmap(pictureBox1.Image);
            float[,] kernel = {
                { 0, -1,  0 },
                { -1, 5, -1 },
                { 0, -1,  0 }
            };

            Bitmap kernelImg = filtrosFourierHelper.CrearImagenDesdeKernel(kernel, original.Width, original.Height);
            pictureBox1.Image = filtrosFourierHelper.ConvolucionFFT(original, kernelImg);
        }

        private void convoluciónDirectaToolStripMenuItem_Click(object sender, EventArgs e) =>
            AplicarFiltro(filtrosFourierHelper.ConvolucionDirecta);

        private void compararConvolucionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null) return;

            Bitmap original = new Bitmap(pictureBox1.Image);
            Bitmap diferencia = filtrosFourierHelper.CompararConvoluciones(original);

            pictureBox1.Image = diferencia;
        }

        #endregion

        private void tamañoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null) return;
            Bitmap redimensionada = ajustesImagen.RedimensionarImagen(new Bitmap(pictureBox1.Image), 512, 512);
            pictureBox1.Image = redimensionada;
        }

       

        private void highboostToolStripMenuItem_Click(object sender, EventArgs e) =>
            AplicarFiltroConDialog(img => new HightBostDialog(img));

        private void difusionAsinotropicaToolStripMenuItem_Click(object sender, EventArgs e) =>
            AplicarFiltroConDialog(img => new difusionAsinotropica(img));

        private void realceLaplacianoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AplicarFiltro(filtrosPractico2.AplicarRealceConLaplaciano);
        }
        //artistico estilos
        private void pinturaOleoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AplicarFiltro(filtrosArtisticos.PinturaOleo);
        }

        private void pixeladoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AplicarFiltro(filtrosArtisticos.Pixelar);
        }
        //agregado ruido
        private void salYPimietaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }



        //deteccion bordes
        private void robertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AplicarFiltro(filtrosDetecionBordes.AplicarRoberts);
        }

        private void prewitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AplicarFiltro(filtrosDetecionBordes.AplicarDifference);
        }

        private void sobelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AplicarFiltro(filtrosDetecionBordes.AplicarSobel);
        }

        private void laplacianoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AplicarFiltro(filtrosDetecionBordes.AplicarLaplaciano);
        }

        private void laplacianoGaussToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AplicarFiltro(filtrosDetecionBordes.AplicarLaplacianoGauss);
        }

        private void cannyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AplicarFiltro(filtrosDetecionBordes.AplicarCanny);
        }
    }
}
