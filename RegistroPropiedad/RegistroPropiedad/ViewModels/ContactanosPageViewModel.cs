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
    public class ContactanosPageViewModel : VistaModeloBase
    {
        public ICommand RegresarCommand { get; set; }
        public ICommand EnviarCorreoCommand { get; set; }
        public ICommand LlamarCommand { get; set; }
        public ICommand EnviarMensajeCommand { get; set; }

        public ContactanosPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
        {
            _navigation = navigationService;
            _dialogService = dialogService;
            RegresarCommand = new Command(async () => { await OnRegresar(); });
            EnviarCorreoCommand = new Command(async () => { await OnEnviarCorreo(); });
            LlamarCommand = new Command(async () => { await OnLlamar(); });
            EnviarMensajeCommand = new Command(async () => { await OnEnviarMensaje(); });
            DisplayNavigationBar = false;
        }

        private async Task OnEnviarCorreo()
        {
            try
            {
                List<string> Tos = new List<string>();
                Tos.Add("epmregistrodelapropiedadportoviejo@rpp.gob.ec");
                var message = new EmailMessage
                {
                    Subject = "",
                    To = Tos
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

        private async Task OnLlamar()
        {
            try
            {
                PhoneDialer.Open("053702150");
            }
            catch (ArgumentNullException anEx)
            {
                // Number was null or white space
            }
            catch (FeatureNotSupportedException ex)
            {
                // Phone Dialer is not supported on this device.
                await DisplayMessage("Info", "La caracteristica de marcado no está soportada en este dispositivo");
            }
            catch (Exception ex)
            {
                // Other error has occurred.
                await DisplayError(ex);
            }
        }

        private async Task OnEnviarMensaje()
        {
            try
            {
                var message = new SmsMessage("", new[] { "053702150" });
                await Sms.ComposeAsync(message);
            }
            catch (FeatureNotSupportedException ex)
            {
                // Sms is not supported on this device.
                await DisplayMessage("Info", "La caracteristica de envio de mensajes no está soportada en este dispositivo");
            }
            catch (Exception ex)
            {
                await DisplayError(ex);
            }
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            if (parameters.ContainsKey("DisplayNavigationBar"))
            {
                DisplayNavigationBar = (bool)parameters["DisplayNavigationBar"];
                //RaisePropertyChanged(nameof(Model));
            }
        }

        private async Task OnRegresar()
        {
            await _navigation.GoBackAsync();
        }
    }
}
