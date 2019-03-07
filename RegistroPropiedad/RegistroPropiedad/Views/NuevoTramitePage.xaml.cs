using System;
using System.Collections.Generic;
using Plugin.Media;
using Xamarin.Forms;

namespace RegistroPropiedad.Views
{
    public partial class NuevoTramitePage : ContentPage
    {
        public NuevoTramitePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<string>(this, "TomarFoto1", async (parametro) =>
            {
                await CrossMedia.Current.Initialize();

                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    await DisplayAlert("Info", "El permiso a fotos está negado, activalo por favor en la configuración de tu dispositivo", "OK");
                    return;
                }

                var file = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    Directory = "MovilRegistro",
                    Name = $"Image1{DateTime.Now.ToShortTimeString()}.jpg"
                });


                if (file == null)
                    return;

                Imagen1.Source = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    file.Dispose();
                    return stream;
                });
                Imagen1.IsVisible = true;
                btnImagen1.IsVisible = false;

            });
            MessagingCenter.Subscribe<string>(this, "TomarFoto2", async (parametro) =>
            {
                await CrossMedia.Current.Initialize();

                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    await DisplayAlert("Info", "El permiso a fotos está negado, activalo por favor en la configuración de tu dispositivo", "OK");
                    return;
                }

                var file = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    Directory = "MovilRegistro",
                    Name = $"Image2{DateTime.Now.ToShortTimeString()}.jpg"
                });


                if (file == null)
                    return;

                Imagen2.Source = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    file.Dispose();
                    return stream;
                });
                Imagen2.IsVisible = true;
                btnImagen2.IsVisible = false;
            });

            MessagingCenter.Subscribe<string>(this, "TomarFoto3", async (parametro) =>
            {
                await CrossMedia.Current.Initialize();

                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    await DisplayAlert("Info", "El permiso a fotos está negado, activalo por favor en la configuración de tu dispositivo", "OK");
                    return;
                }

                //var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                //{
                //    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,

                //});
                var file = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    Directory = "MovilRegistro",
                    Name = $"Image3{DateTime.Now.ToShortTimeString()}.jpg"
                });

                if (file == null)
                    return;

                Imagen3.Source = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    file.Dispose();
                    return stream;
                });
                Imagen3.IsVisible = true;
                btnImagen3.IsVisible = false;
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<string>(this, "TomarFoto1");
            MessagingCenter.Unsubscribe<string>(this, "TomarFoto2");
            MessagingCenter.Unsubscribe<string>(this, "TomarFoto3");
        }
    }
}
