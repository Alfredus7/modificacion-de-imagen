using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;

public class AplicadorFiltros
{
    private readonly ProgressBar _progressBar;

    public AplicadorFiltros(ProgressBar progressBar)
    {
        _progressBar = progressBar;
    }

    public async Task<Bitmap> AplicarFiltroAsync(Bitmap imagen, Func<Bitmap, Bitmap> filtro)
    {
        _progressBar.Visible = true;
        _progressBar.Style = ProgressBarStyle.Marquee;
        try
        {
            return await Task.Run(() => filtro(new Bitmap(imagen)));
        }
        finally
        {
            _progressBar.Visible = false;
        }
    }

    public async Task<Bitmap> AplicarFiltroConDialogAsync(Bitmap imagen, Func<Bitmap, Form> crearDialogo)
    {
        _progressBar.Visible = true;
        _progressBar.Style = ProgressBarStyle.Marquee;
        try
        {
            var dialog = crearDialogo(new Bitmap(imagen));
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                return ((dynamic)dialog).ImageToProcess;
            }
        }
        finally
        {
            _progressBar.Visible = false;
        }
        return imagen;
    }
}
