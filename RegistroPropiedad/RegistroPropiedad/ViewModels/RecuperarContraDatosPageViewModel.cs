using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Navigation;
using Prism.Services;
using RegistroPropiedad.Helper;
using RegistroPropiedad.Modelos;
using RegistroPropiedad.Servicios;
using Xamarin.Forms;

namespace RegistroPropiedad.ViewModels
{
    public class RecuperarContraDatosPageViewModel : VistaModeloBase
    {
        public ICommand RecuperarCommand { get; set; }
        public ICommand RegresarCommand { get; set; }

        public PerfilUsuarioModelo Modelo { get; set; }
        private UsuarioServicio _servicio;

        public RecuperarContraDatosPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
        {
            _navigation = navigationService;
            _dialogService = dialogService;
            _servicio = new UsuarioServicio();
            RecuperarCommand = new Command(async () => { await OnRecuperar(); });
            RegresarCommand = new Command(async () => { await OnRegresar(); });
            Title = "Verificar Datos";
        }

        private async Task OnRecuperar()
        {
            try
            {
                if (!await CheckInternet())
                {
                    return;
                }

                IsBusy = true;
                var response = await _servicio.RecuperarContra(Modelo.persona.correo1, Modelo.username, Modelo.id);
                if (response.IsSuccess)
                {
                    await DisplayMessage("Info", "En breve recibirá un correo electrónico para resetear su contraseña");
                    IsBusy = false;
                }
                else
                {
                    await DisplayApiMessage(response);
                }
            }
            catch (Exception ex)
            {
                await DisplayError(ex);
            }
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            if (parameters.ContainsKey("usuario"))
            {
                Modelo = (PerfilUsuarioModelo)parameters["usuario"];
                RaisePropertyChanged(nameof(Modelo));
            }
        }

        private async Task OnRegresar()
        {
            try
            {
                await _navigation.GoBackAsync();
            }
            catch (Exception ex)
            {
                await DisplayError(ex);
            }
        }

    }
}
