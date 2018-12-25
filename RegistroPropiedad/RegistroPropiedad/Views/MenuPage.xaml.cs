using System;
using System.Collections.Generic;
using FFImageLoading.Svg.Forms;
using RegistroPropiedad.CustomControls;
using RegistroPropiedad.ViewModels;
using Xamarin.Forms;

namespace RegistroPropiedad.Views
{
    public partial class MenuPage : ContentPage
    {
        //public ListView ListView { get { return listView; } }

        //ListView listView;

        //private readonly FFImageLoading.Svg.Forms.SvgImageSourceConverter _imageSourceConverter = new SvgImageSourceConverter();

        public MenuPage()
        {
            InitializeComponent();
            this.BindingContext = new MenuPageViewModel();
        }
    }
}
