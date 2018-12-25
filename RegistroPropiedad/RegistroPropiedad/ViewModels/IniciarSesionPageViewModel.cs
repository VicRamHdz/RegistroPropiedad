using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms;

namespace RegistroPropiedad.ViewModels
{
    public class IniciarSesionPageViewModel : VistaModeloBase
    {
        public ICommand RegresarCommand { get; set; }

        public IniciarSesionPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
        {
            _navigation = navigationService;
            _dialogService = dialogService;
            RegresarCommand = new Command(async () => { await OnRegresar(); });
            Title = "Iniciar sesion";
        }

        private async Task OnRegresar()
        {
            await _navigation.GoBackAsync();
        }
    }
}
