using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SmartStore.Clases;

namespace SmartStore.Paginas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaginaLibro : ContentPage
    {
        public Libros Libro;
        Recomendaciones recomendaciones;
        List<Emociones> emociones;

        public PaginaLibro()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = Libro;
            recomendaciones = new Recomendaciones();
            await ObtenerRecomendaciones();
            await ObtenerEmociones();
        }

        async Task ObtenerRecomendaciones()
        {
            lsvLibros.ItemsSource = null;
            int num = 5;
            var libros = new List<Libros>();
            var resultado = await recomendaciones.ObtenerRecomendaciones(Libro.Id, num);

            foreach (var info in resultado.RecommendedItemSetInfo)
            {
                foreach (var item in info.Items)
                {
                    Libros libro = App.Catalogo.ObtenerLibro(item.Id);

                    if (libro != null)
                        libro.Score = info.Rating.ToString("N2");

                    libros.Add(libro);
                }
            }

            lsvLibros.ItemsSource = libros;
        }

        async Task ObtenerEmociones()
        {
            lsvEmociones.ItemsSource = null;
            emociones = await App.ServicioAzure.GetEmocionesAsync(Libro.Id);
            lsvEmociones.ItemsSource = emociones;
        }
        
        private void lsvLibros_Selected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                PaginaLibro pagina = new PaginaLibro();
                pagina.Libro = e.SelectedItem as Libros;
                Navigation.PushAsync(pagina);
            }
        }

        private async void btnEmocion_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PaginaEmocion(Libro.Id));
        }

        private void lsvEmociones_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }
    }
}
