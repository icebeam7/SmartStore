using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using SmartStore.Clases;
using SmartStore.Paginas;
using System.Threading.Tasks;
using SmartStore.Servicios;

namespace SmartStore
{
    public partial class App : Application
    {
        public static int Pagina;
        public static Catalogo Catalogo;
        public static ServicioAzure ServicioAzure;

        public interface IAuthenticate
        {
            Task<bool> Authenticate();
        }

        public static IAuthenticate Authenticator { get; private set; }

        public static void Init(IAuthenticate authenticator)
        {
            Authenticator = authenticator;
        }


        public App()
        {
            InitializeComponent();

            Pagina = 1;
            Catalogo = new Catalogo();
            MainPage = new NavigationPage(new Paginas.MainPage());
            ServicioAzure = new ServicioAzure();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
