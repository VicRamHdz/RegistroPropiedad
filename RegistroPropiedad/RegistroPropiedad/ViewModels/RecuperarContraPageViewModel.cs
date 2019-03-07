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
    public class RecuperarContraPageViewModel : VistaModeloBase
    {
        private string _Cedula;
        public string Cedula
        {
            get { return _Cedula; }
            set
            {
                SetProperty(ref _Cedula, value);
            }
        }

        public ICommand SiguienteCommand { get; set; }
        public ICommand RegresarCommand { get; set; }

        private UsuarioServicio _servicio;
        private PerfilUsuarioModelo _modelo;
        public RecuperarContraPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
        {
            _navigation = navigationService;
            _dialogService = dialogService;
            _servicio = new UsuarioServicio();
            SiguienteCommand = new Command(async () => { await OnSiguiente(); });
            RegresarCommand = new Command(async () => { await OnRegresar(); });
            Title = "Recuperar Contraseña";
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
                    await DisplayMessage("Info", "Cedula es obligatoria");
                    return;
                }

                IsBusy = true;
                var response = await _servicio.ValidarExistenciaCedula(Cedula);
                if (response.IsSuccess)
                {
                    _modelo = response.Data;
                    NavigationParameters parametros = new NavigationParameters();
                    parametros.Add("usuario", _modelo);
                    await _navigation.NavigateAsync("RecuperarContraDatosPage", parametros);
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
