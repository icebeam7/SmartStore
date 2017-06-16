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
    public partial class PaginaListaLibros : ContentPage
    {
        public PaginaListaLibros()
        {
            InitializeComponent();
            lsvLibros.ItemsSource = App.Catalogo.ObtenerLibros(App.Pagina);
            lblTitulo.Text = $"Libros - Pagina {App.Pagina}";
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            // Solo lo use al inicio para insertar los 17000 registros: await App.Catalogo.InsertarLibros();
        }

        private void btnAvanzar_Click(object sender, EventArgs a)
        {
            if (App.Pagina < App.Catalogo.Paginas)
                CambiarPagina(1);
        }

        private void btnRetroceder_Click(object sender, EventArgs a)
        {
            if (App.Pagina > 1)
                CambiarPagina(-1);
        }

        private void CambiarPagina(int incremento)
        {
            lsvLibros.ItemsSource = null;
            App.Pagina += incremento;
            lsvLibros.ItemsSource = App.Catalogo.ObtenerLibros(App.Pagina);
            lblTitulo.Text = $"Libros - Pagina {App.Pagina}";
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
    }
}
