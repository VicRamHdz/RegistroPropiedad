using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms;

namespace RegistroPropiedad.ViewModels
{
    public class InicialPageViewModel : VistaModeloBase
    {
        public ICommand IniciarSesionCommand { get; set; }

        public InicialPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
        {
            _navigation = navigationService;
            _dialogService = dialogService;
            IniciarSesionCommand = new Command(async () => { await OnIniciarSesion(); });
        }

        private async Task OnIniciarSesion()
        {
            await _navigation.NavigateAsync("IniciarSesionPage");
        }
    }
}
