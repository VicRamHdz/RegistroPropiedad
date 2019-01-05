using System;
using System.Threading.Tasks;
using System.Windows.Input;
using RegistroPropiedad.Helper;
using RegistroPropiedad.Views;
using Xamarin.Forms;

namespace RegistroPropiedad.ViewModels
{
    public class MenuPageViewModel : VistaModeloBase
    {
        public ICommand CerrarSesionCommand { get; set; }

        private string _NombreUsuario;
        public string NombreUsuario
        {
            get { return _NombreUsuario; }
            set
            {
                SetProperty(ref _NombreUsuario, value);
            }
        }

        public MenuPageViewModel()
        {
            CerrarSesionCommand = new Command(async () => { await OnCerrarSesion(); });
            NombreUsuario = $"{App.PerfilUsuarioInfo?.persona.nombres} {App.PerfilUsuarioInfo?.persona.apellidos}";
        }

        private async Task OnCerrarSesion()
        {
            try
            {
                if (await DisplayYesNoMessage("Atención", "¿Está seguro que desea cerrar sesión?"))
                {
                    IsBusy = true;
                    //BusyMessage = "Cerrando Sesión";
                    //remove user secure data from local database
                    App.PerfilUsuarioInfo = null;
                    CacheHelper.Cache.InvalidateAll();
                    await Task.Delay(1000);
                    //redirect to landing page
                    ((App)Application.Current).MainPage = new InicialPage();
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
    }
}