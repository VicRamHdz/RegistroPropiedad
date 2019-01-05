using System;
using System.Collections.Generic;
using RegistroPropiedad.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace RegistroPropiedad.Views
{
    public partial class UbicacionPage : ContentPage
    {
        public UbicacionPage()
        {
            InitializeComponent();
            MapView.MoveToRegion(
            MapSpan.FromCenterAndRadius(
             new Position(-1.0520576, -80.4598898), Distance.FromMiles(1)));
        }

        private void Street_OnClicked(object sender, EventArgs e)
        {
            MapView.MapType = MapType.Street;
        }

        private void Hybrid_OnClicked(object sender, EventArgs e)
        {
            MapView.MapType = MapType.Hybrid;
        }

        private void Satellite_OnClicked(object sender, EventArgs e)
        {
            MapView.MapType = MapType.Satellite;
        }
    }
}
