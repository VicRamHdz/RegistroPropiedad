using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace RegistroPropiedad.Views
{
    public partial class PublicidadPage : ContentPage
    {
        public PublicidadPage()
        {
            InitializeComponent();

            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            webview.Source = "http://www.registropropiedadportoviejo.gob.ec/regpp/index.php/inicio/noticias";
        }

        #region Metodos
        void webOnNavigating(object sender, WebNavigatingEventArgs e)
        {
            MyActivityIndicator.IsRunning = MyActivityIndicator.IsVisible = true;
        }

        void webEndNavigating(object sender, WebNavigatedEventArgs e)
        {
            MyActivityIndicator.IsRunning = MyActivityIndicator.IsVisible = false;

        }
        #endregion
    }
}
