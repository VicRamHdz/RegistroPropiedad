using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Navigation;
using Prism.Services;
using RegistroPropiedad.Helper;
using RegistroPropiedad.Servicios;
using RegistroPropiedad.Views;
using Xamarin.Forms;

namespace RegistroPropiedad.ViewModels
{
    public class IniciarSesionPageViewModel : VistaModeloBase
    {
        public ICommand RegresarCommand { get; set; }
        public ICommand IngresarCommand { get; set; }
        public ICommand RecuperarContraCommand { get; set; }

        private string _Identificacion;
        public string Identificacion
        {
            get { return _Identificacion; }
            set
            {
                SetProperty(ref _Identificacion, value);
            }
        }

        private string _Contra;
        public string Contra
        {
            get { return _Contra; }
            set
            {
                SetProperty(ref _Contra, value);
            }
        }

        private UsuarioServicio _servicio;

        public IniciarSesionPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
        {
            _navigation = navigationService;
            _dialogService = dialogService;
            RegresarCommand = new Command(async () => { await OnRegresar(); });
            IngresarCommand = new Command(async () => { await OnIngresar(); });
            RecuperarContraCommand = new Command(async () => { await OnRecuperarContra(); });
            Title = "Iniciar sesion";
            _servicio = new UsuarioServicio();
        }

        private async Task OnIngresar()
        {
            try
            {
                if (!await CheckInternet())
                {
                    return;
                }
                if (Identificacion.IsNullOrEmpty())
                {
                    await DisplayMessage("Info", "Introduzca el numero de indentificación");
                    return;
                }
                if (Contra.IsNullOrEmpty())
                {
                    await DisplayMessage("Info", "Introduzca su contraseña");
                    return;
                }

                IsBusy = true;
                //BusyMessage = "Iniciando...";
                var response = await _servicio.IniciarSesion(Identificacion, Contra);
                if (response.IsSuccess)
                {
                    if (response.Data == null)
                    {
                        await DisplayMessage("Info", "Identificación no existe ó contraseña incorrecta");
                        return;
                    }
                    App.PerfilUsuarioInfo = response.Data;
                    await CacheHelper.UpdateCache("PerfilUsuarioInfo", App.PerfilUsuarioInfo);
                    Application.Current.MainPage = new PrincipalPage(_navigation, _dialogService);
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

        private async Task OnRecuperarContra()
        {
            try
            {
                await _navigation.NavigateAsync("RecuperarContraPage");
            }
            catch (Exception ex)
            {
                await DisplayError(ex);
            }
        }

        private async Task OnRegresar()
        {
            await _navigation.GoBackAsync();
        }
    }
}
