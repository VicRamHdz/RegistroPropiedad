using System;
using System.Collections.Generic;
using Microcharts;
using RegistroPropiedad.ViewModels;
using SkiaSharp;
using Xamarin.Forms;

namespace RegistroPropiedad.Views
{
    public partial class TramiteDetallePage : ContentPage
    {
        public TramiteDetallePage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<string>(this, "CalcularGrafica", (parametro) =>
            {
                if (this.BindingContext != null)
                {
                    var vm = this.BindingContext as TramiteDetallePageViewModel;
                    var entries = new[]
                    {
                new Microcharts.Entry(vm.Model.porcentajeavance)
                {
                    Label = "Completado",
                    ValueLabel = $"{vm.Model.porcentajeavance.ToString()} %",
                    TextColor = vm.Model.porcentajeavance == 100 ? SKColor.Parse("#000000") : SKColor.Parse("ff0000"),
                    Color = vm.Model.porcentajeavance == 100 ? SKColor.Parse("#73cf44") : SKColor.Parse("ff0000")
                }
                };

                    var chart = new RadialGaugeChart()
                    {
                        Entries = entries,
                        MinValue = 0,
                        MaxValue = 100,
                        LabelTextSize = 47f,
                        Margin = 0f
                    };
                    this.chartView.Chart = chart;
                }
            });
        }
    }
}
