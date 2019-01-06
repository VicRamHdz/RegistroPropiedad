using System;
using System.Collections.Generic;
using RegistroPropiedad.ViewModels;
using Xamarin.Forms;

namespace RegistroPropiedad.Views
{
    public partial class VerDocumentoPage : ContentPage
    {
        string Url;

        public VerDocumentoPage(string Url) : base()
        {
            this.Url = Url;
        }

        public VerDocumentoPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (this.BindingContext != null)
            {
                (this.BindingContext as VerDocumentoPageViewModel).UrlDocumento = Url;
            }
        }

        void webStar(object sender, WebNavigatingEventArgs e)
        {
            ActivityIndicator.IsRunning = ActivityIndicator.IsVisible = true;
        }

        void webStop(object sender, WebNavigatedEventArgs e)
        {
            ActivityIndicator.IsRunning = ActivityIndicator.IsVisible = false;
        }
    }
}
