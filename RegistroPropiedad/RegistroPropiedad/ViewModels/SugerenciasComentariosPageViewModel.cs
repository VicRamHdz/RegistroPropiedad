using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace RegistroPropiedad.ViewModels
{
    public class SugerenciasComentariosPageViewModel : VistaModeloBase
    {
        public ICommand EnviarCommand { get; set; }

        private string _Nombre;
        public string Nombre
        {
            get
            {
                return _Nombre;
            }
            set
            {
                SetProperty(ref _Nombre, value);
            }
        }

        private string _Correo;
        public string Correo
        {
            get
            {
                return _Correo;
            }
            set
            {
                SetProperty(ref _Correo, value);
            }
        }

        private string _Mensaje;
        public string Mensaje
        {
            get
            {
                return _Mensaje;
            }
            set
            {
                SetProperty(ref _Mensaje, value);
            }
        }

        public SugerenciasComentariosPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
        {
            _navigation = navigationService;
            _dialogService = dialogService;
            EnviarCommand = new Command(async () => { await OnEnviar(); });
        }

        private async Task OnEnviar()
        {
            try
            {
                if (string.IsNullOrEmpty(Nombre) || string.IsNullOrEmpty(Correo)
                    || string.IsNullOrEmpty(Mensaje))
                {
                    await DisplayMessage("Info", "Debe llenar los campos de Nombre, Correo y el Mensaje a enviar");
                    return;
                }
                List<string> Tos = new List<string>();
                Tos.Add("epmregistrodelapropiedadportoviejo@rpp.gob.ec");

                var message = new EmailMessage
                {
                    Subject = $"Sugerencia de Contacto",
                    To = Tos,
                    Body = $"Nombre:{ Nombre } \n Email: {Correo} \n Mensaje: {Mensaje}"
                };
                await Email.ComposeAsync(message);
            }
            catch (FeatureNotSupportedException fbsEx)
            {
                // Email is not supported on this device
                await DisplayMessage("Info", "Correo no soportado");
            }
            catch (Exception ex)
            {
                // Some other exception occurred
                await DisplayError(ex);
            }
        }

    }
}
