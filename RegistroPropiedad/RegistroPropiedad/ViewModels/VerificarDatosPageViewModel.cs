using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Navigation;
using Prism.Services;
using RegistroPropiedad.Helper;
using RegistroPropiedad.Modelos;
using Xamarin.Forms;

namespace RegistroPropiedad.ViewModels
{
    public class VerificarDatosPageViewModel : VistaModeloBase
    {
        public ICommand RegresarCommand { get; set; }
        public ICommand SiguienteCommand { get; set; }

        private PerfilUsuarioModelo _Model;
        public PerfilUsuarioModelo Model
        {
            get { return _Model; }
            set
            {
                SetProperty(ref _Model, value);
            }
        }

        public VerificarDatosPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
        {
            _navigation = navigationService;
            _dialogService = dialogService;
            RegresarCommand = new Command(async () => { await OnRegresar(); });
            SiguienteCommand = new Command(async () => { await OnSiguiente(); });
            Title = "Verifica tus Datos";
        }

        private async Task OnSiguiente()
        {
            try
            {
                if (!await CheckInternet())
                {
                    return;
                }

                if (Model.persona.nombres.IsNullOrEmpty())
                {
                    await DisplayMessage("Info", "Ingrese nombres");
                    return;
                }

                if (Model.persona.apellidos.IsNullOrEmpty())
                {
                    await DisplayMessage("Info", "Ingrese apellidos");
                    return;
                }

                if (Model.persona.direccion.IsNullOrEmpty())
                {
                    await DisplayMessage("Info", "Ingrese dirección");
                    return;
                }

                if (Model.persona.telefono1.IsNullOrEmpty())
                {
                    await DisplayMessage("Info", "Ingrese celular");
                    return;
                }

                if (Model.persona.correo1.IsNullOrEmpty())
                {
                    await DisplayMessage("Info", "Ingrese correo electrónico");
                    return;
                }

                NavigationParameters parametros = new NavigationParameters();
                parametros.Add("DatosUsuario", Model);
                await _navigation.NavigateAsync("CrearContraPage", parametros);
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

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            if (parameters.ContainsKey("DatosUsuario"))
            {
                Model = (PerfilUsuarioModelo)parameters["DatosUsuario"];
                RaisePropertyChanged(nameof(Model));
            }
        }

        private async Task OnRegresar()
        {
            await _navigation.GoBackAsync();
        }
    }
}
