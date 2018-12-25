using System;
using System.Collections.Generic;
using Akavache;
using System.Reactive.Linq;
using RegistroPropiedad.Helper;
using RegistroPropiedad.Modelos;
using Xamarin.Forms;
using RegistroPropiedad.ViewModels;

namespace RegistroPropiedad.Views
{
    public partial class InicialPage : ContentPage
    {
        public InicialPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            if (App.PerfilUsuarioInfo == null)
            {
                App.PerfilUsuarioInfo = CacheHelper.Cache.GetObject<PerfilUsuarioModelo>("PerfilUsuarioInfo").Catch(Observable.Return(default(PerfilUsuarioModelo))).Wait();
            }
            if (App.PerfilUsuarioInfo != null)
            {
                Application.Current.MainPage = new PrincipalPage((this.BindingContext as InicialPageViewModel)._navigation, (this.BindingContext as InicialPageViewModel)._dialogService);
            }
            base.OnAppearing();
        }
    }
}
