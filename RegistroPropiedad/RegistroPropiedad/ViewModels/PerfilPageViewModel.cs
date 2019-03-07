using System;
using Prism.Navigation;
using Prism.Services;
using RegistroPropiedad.Modelos;

namespace RegistroPropiedad.ViewModels
{
    public class PerfilPageViewModel : VistaModeloBase
    {
        public PerfilUsuarioModelo Modelo { get; set; }

        public PerfilPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
        {
            _navigation = navigationService;
            _dialogService = dialogService;

            Modelo = App.PerfilUsuarioInfo;
        }
    }
}
