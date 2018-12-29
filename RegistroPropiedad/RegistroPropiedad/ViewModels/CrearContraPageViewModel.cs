using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Navigation;
using Prism.Services;
using RegistroPropiedad.Helper;
using RegistroPropiedad.Modelos;
using RegistroPropiedad.Servicios;
using RegistroPropiedad.Views;
using Xamarin.Forms;

namespace RegistroPropiedad.ViewModels
{
    public class CrearContraPageViewModel : VistaModeloBase
    {
        public ICommand RegresarCommand { get; set; }
        public ICommand FinalizarCommand { get; set; }

        private string _Contra;
        public string Contra
        {
            get { return _Contra; }
            set
            {
                SetProperty(ref _Contra, value);
            }
        }

        private string _ContraConfirmar;
        public string ContraConfirmar
        {
            get { return _ContraConfirmar; }
            set
            {
                SetProperty(ref _ContraConfirmar, value);
            }
        }

        private PerfilUsuarioModelo _Model;
        public PerfilUsuarioModelo Model
        {
            get { return _Model; }
            set
            {
                SetProperty(ref _Model, value);
            }
        }

        private UsuarioServicio _servicio;

        public CrearContraPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
        {
            _navigation = navigationService;
            _dialogService = dialogService;
            RegresarCommand = new Command(async () => { await OnRegresar(); });
            FinalizarCommand = new Command(async () => { await OnFinalizar(); });
            _servicio = new UsuarioServicio();
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            if (parameters.ContainsKey("DatosUsuario"))
            {
                Model = (PerfilUsuarioModelo)parameters["DatosUsuario"];
                RaisePropertyChanged(nameof(Model));
            }
        }

        private async Task OnFinalizar()
        {
            try
            {
                if (!await CheckInternet())
                {
                    return;
                }

                if (Contra.IsNullOrEmpty())
                {
                    await DisplayMessage("Info", "Ingrese su contraseña");
                    return;
                }

                if (ContraConfirmar.IsNullOrEmpty())
                {
                    await DisplayMessage("Info", "Confirme su contraseña");
                    return;
                }

                if (Contra != ContraConfirmar)
                {
                    await DisplayMessage("Info", "Contraseñas no coinciden");
                    return;
                }

                IsBusy = true;
                BusyMessage = "Creando perfil...";
                Model.pass = Contra;
                var response = await _servicio.CrearPerfilUsuario(Model);
                if (response.IsSuccess)
                {
                    App.PerfilUsuarioInfo = Model;
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
        }

        private async Task OnRegresar()
        {
            await _navigation.GoBackAsync();
        }
    }
}
