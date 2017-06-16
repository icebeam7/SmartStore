using Gcm.Client;

using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Plugin.Permissions;

using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;
using SmartStore;

namespace SmartStore.Droid
{
    [Activity(Label = "SmartStore", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, SmartStore.App.IAuthenticate
    {
        private MobileServiceUser user;

        public async Task<bool> Authenticate()
        {
            var success = false;
            var message = "";

            try
            {
                user = await SmartStore.App.ServicioAzure.Client.LoginAsync(this, MobileServiceAuthenticationProvider.Facebook);

                if (user != null)
                {
                    message = string.Format("Tu id es {0}", user.UserId);
                    success = true;
                }
            }
            catch (Exception ex) { }

            CreateAndShowDialog(message, "Inicio de sesion");
            return success;
        }

        private void CreateAndShowDialog(string message, string title)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(this);

            builder.SetMessage(message);
            builder.SetTitle(title);
            builder.Create().Show();
        }

        static MainActivity instance = null;

        protected override void OnCreate(Bundle bundle)
        {
            // GCM
            instance = this;
            /////

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            SmartStore.App.Init((SmartStore.App.IAuthenticate)this);
            LoadApplication(new SmartStore.App());

            // GCM
            try
            {
                GcmClient.CheckDevice(this);
                GcmClient.CheckManifest(this);
                System.Diagnostics.Debug.WriteLine("Registering...");
                GcmClient.Register(this, SmartStoreBroadcastReceiver.senderIDs);
            }
            catch (Java.Net.MalformedURLException)
            {
                CreateAndShowDialog("There was an error creating the client. Verify the URL.", "Error");
            }
            catch (Exception e)
            {
                CreateAndShowDialog(e.Message, "Error");
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public static MainActivity CurrentActivity
        {
            get { return instance; }
        }

    }
}