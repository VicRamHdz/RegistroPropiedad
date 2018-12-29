using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Navigation;
using Prism.Services;
using RegistroPropiedad.Helper;
using RegistroPropiedad.Servicios;
using Xamarin.Forms;

namespace RegistroPropiedad.ViewModels
{
    public class RegistroPageViewModel : VistaModeloBase
    {
        public ICommand RegresarCommand { get; set; }
        public ICommand SiguienteCommand { get; set; }

        private string _Cedula;
        public string Cedula
        {
            get { return _Cedula; }
            set
            {
                SetProperty(ref _Cedula, value);
            }
        }

        private UsuarioServicio _servicio;

        public RegistroPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
        {
            _navigation = navigationService;
            _dialogService = dialogService;
            RegresarCommand = new Command(async () => { await OnRegresar(); });
            SiguienteCommand = new Command(async () => { await OnSiguiente(); });
            Title = "Registro de Usuario";
            _servicio = new UsuarioServicio();
        }

        private async Task OnSiguiente()
        {
            try
            {
                if (!await CheckInternet())
                {
                    return;
                }
                if (Cedula.IsNullOrEmpty())
                {
                    await DisplayMessage("Info", "Ingrese el numero de cédula");
                    return;
                }
                IsBusy = true;
                BusyMessage = "Buscando Cédula...";
                var response = await _servicio.ValidarExistenciaCedula(Cedula);
                if (response.IsSuccess)
                {
                    if (response.Data == null)
                    {
                        await DisplayMessage("Info", "Cédula no existe");
                        return;
                    }

                    NavigationParameters parametros = new NavigationParameters();
                    parametros.Add("DatosUsuario", response.Data);
                    await _navigation.NavigateAsync("VerificarDatosPage", parametros);
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
            finally
            {
                IsBusy = false;
            }
        }

        private async Task OnRegresar()
        {
            await _navigation.GoBackAsync();
        }
    }
}
