using System.Drawing;
using System.IO;
using System.Windows.Forms;

public class ImagenManager
{
    public string NombreArchivoCargado { get; private set; }
    public Bitmap ImagenOriginal { get; private set; }

    public Bitmap CargarImagen(out string nombreArchivo)
    {
        OpenFileDialog ofd = new OpenFileDialog { Filter = @"Archivos de imagen|*.jpg;*.png;*.jpeg" };
        if (ofd.ShowDialog() == DialogResult.OK)
        {
            NombreArchivoCargado = Path.GetFileNameWithoutExtension(ofd.FileName);
            ImagenOriginal = new Bitmap(ofd.FileName);
            nombreArchivo = NombreArchivoCargado;
            return new Bitmap(ImagenOriginal);
        }
        nombreArchivo = null;
        return null;
    }

    public void GuardarImagen(Image imagen, string nombreArchivo)
    {
        SaveFileDialog sfd = new SaveFileDialog
        {
            Filter = "Imagen JPEG|*.jpg|Imagen PNG|*.png",
            Title = "Guardar imagen",
            FileName = nombreArchivo + " Copia"
        };

        if (sfd.ShowDialog() == DialogResult.OK)
        {
            string ext = Path.GetExtension(sfd.FileName).ToLower();
            var formato = ext == ".jpg" ? System.Drawing.Imaging.ImageFormat.Jpeg : System.Drawing.Imaging.ImageFormat.Png;
            imagen.Save(sfd.FileName, formato);
        }
    }

    public Bitmap RevertirImagen()
    {
        return ImagenOriginal != null ? new Bitmap(ImagenOriginal) : null;
    }
}

