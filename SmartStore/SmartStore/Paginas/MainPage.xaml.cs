using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartStore.Paginas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        bool authenticated = false;

        public MainPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            if (authenticated)
                await Navigation.PushAsync(new Paginas.PaginaListaLibros());
        }

        async void Facebook_Clicked(object s, EventArgs e)
        {
            if (App.Authenticator != null)
              authenticated = await App.Authenticator.Authenticate();

            if (authenticated)
            {
                await Navigation.PushAsync(new Paginas.PaginaListaLibros());
            }
        }

    }
}
