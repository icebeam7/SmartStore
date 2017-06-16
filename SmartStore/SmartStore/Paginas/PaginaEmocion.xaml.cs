using System;
using System.Collections.Generic;
using System.IO;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartStore.Paginas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaginaEmocion : ContentPage
    {
        public PaginaEmocion(string id)
        {
            InitializeComponent();
            libro = id;
        }

        static Stream streamCopy;
        public string libro;

        private async void btnCamera_Clicked(object sender, EventArgs e)
        {
            var text = ((Button)sender).Text;
            var useCamara = text.Contains("camara");

            var file = await Servicios.ServicioImagen.TakePicture(useCamara);
            
            lblResult.Text = "---";

            imgPic.Source = ImageSource.FromStream(() => {
                var stream = file.GetStream();
                streamCopy = new MemoryStream();
                stream.CopyTo(streamCopy);
                stream.Seek(0, SeekOrigin.Begin);
                file.Dispose();
                return stream;
            });

            btnAnalizarEmocion.IsEnabled = true;
        }

        SmartStore.Clases.Emociones emocion;
        KeyValuePair<string, float> parEmocion;

        private async void btnAnalizarEmocion_Clicked(object sender, EventArgs e)
        {
            ShowProgress(true);

            if (streamCopy != null)
            {
                streamCopy.Seek(0, SeekOrigin.Begin);

                parEmocion = await Servicios.ServicioEmocion.GetEmotions(streamCopy);

                lblResult.Text = parEmocion.Key + " " + parEmocion.Value;
                btnGuardar.IsEnabled = true;
                btnAnalizarEmocion.IsEnabled = false;
            }
            else lblResult.Text = "---No image select---";

            ShowProgress(false);
        }

        void ShowProgress(bool show)
        {
            actProgress.IsVisible = show;
            actProgress.IsRunning = show;
            btnCamera.IsEnabled = !show;
            btnGallery.IsEnabled = !show;
            btnAnalizarEmocion.IsEnabled = !show;
            btnGuardar.IsEnabled = !show;
        }

        private async void btnGuardar_Clicked(object sender, EventArgs e)
        {
            ShowProgress(true);

            btnGuardar.IsEnabled = false;

            emocion = new Clases.Emociones();
            emocion.Emocion = parEmocion.Key;
            emocion.Score = parEmocion.Value;
            emocion.Libro = libro;

            if (streamCopy != null)
            {
                streamCopy.Seek(0, SeekOrigin.Begin);

                emocion.Foto = await Servicios.ServiceStorage.performBlobOperation(streamCopy);
            }
            else
                emocion.Foto = "";

            await App.ServicioAzure.InsertEmocionAsync(emocion);
            await DisplayAlert("Éxito", "Emocion registrada", "OK");
            ShowProgress(false);
        }
    }
}
