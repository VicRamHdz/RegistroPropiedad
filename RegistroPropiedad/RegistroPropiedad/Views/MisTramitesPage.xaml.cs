using System;
using System.Collections.Generic;
using RegistroPropiedad.Modelos;
using RegistroPropiedad.ViewModels;
using Xamarin.Forms;

namespace RegistroPropiedad.Views
{
    public partial class MisTramitesPage : ContentPage
    {
        MisTramitesPageViewModel _vm;
        int paginaACargar = 0;

        public MisTramitesPage()
        {
            InitializeComponent();
            _vm = (MisTramitesPageViewModel)this.BindingContext;
        }

        async void ListItemAppearing(object sender, Xamarin.Forms.ItemVisibilityEventArgs e)
        {
            if (_vm.IsBusy || _vm.MisTramites.Count == 0)
                return;

            //hit bottom!
            if ((e.Item as TramitesModelo) == _vm.MisTramites[_vm.MisTramites.Count - 1])
            {
                paginaACargar++;
                await _vm.CargarTramites(paginaACargar);
                paginaACargar = _vm.Pagina;
            }
        }
    }
}
