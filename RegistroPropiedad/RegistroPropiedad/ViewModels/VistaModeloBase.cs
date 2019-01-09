using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Plugin.Connectivity;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using RegistroPropiedad.Modelos;
using Xamarin.Forms;

namespace RegistroPropiedad.ViewModels
{
    public class VistaModeloBase : BindableBase, INavigatedAware
    {
        private string _busyMessage = "";

        private bool _isBusy;
        internal bool IsBusy
        {
            get
            {
                return _isBusy;
            }
            set
            {
                _isBusy = value;
                try
                {
                    if (DisplayLoading)
                    {
                        if (IsBusy)
                        {
                            if (string.IsNullOrEmpty(BusyMessage))
                                BusyMessage = "Cargando...";
                            UserDialogs.Instance.ShowLoading(BusyMessage, Device.RuntimePlatform == Device.iOS ? MaskType.Black : MaskType.Clear);
                        }
                        else
                        {
                            UserDialogs.Instance.HideLoading();
                            _busyMessage = "";
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Can not display or hide Loading dialog, Error {ex.Message} Stack: {ex.StackTrace}");
                }
                RaisePropertyChanged();
            }
        }

        internal bool DisplayLoading
        {
            get; set;
        }

        internal string BusyMessage
        {
            get
            {
                return _busyMessage;
            }
            set
            {
                _busyMessage = value;
                if (IsBusy && DisplayLoading)
                {
                    UserDialogs.Instance.ShowLoading(BusyMessage, Device.RuntimePlatform == Device.iOS ? MaskType.Black : MaskType.Clear);
                }
            }
        }

        private bool _DisplayNavigationBar;
        public bool DisplayNavigationBar
        {
            get { return _DisplayNavigationBar; }
            set { SetProperty(ref _DisplayNavigationBar, value); }
        }

        internal INavigationService _navigation { get; set; }
        internal IPageDialogService _dialogService { get; set; }

        private string _Title;
        public string Title { get { return _Title; } set { SetProperty(ref _Title, value); } }

        public VistaModeloBase()
        {
            DisplayLoading = true;
            DisplayNavigationBar = true;
        }

        public virtual void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public virtual void OnNavigatedTo(NavigationParameters parameters)
        {
        }

        public async Task<bool> CheckInternet()
        {
            if (IsBusy)
                IsBusy = false;

            if (!CrossConnectivity.Current.IsConnected)
            {
                await _dialogService?.DisplayAlertAsync("", "No tiene conexión a internet...", "OK");
                return false;
            }
            else return true;
        }

        public async Task DisplayMessage(string title, string message)
        {
            if (IsBusy)
                IsBusy = false;
            await _dialogService?.DisplayAlertAsync(title, message, "OK");
        }

        public async Task<bool> DisplayYesNoMessage(string title, string message)
        {
            if (IsBusy)
                IsBusy = false;
            return await _dialogService?.DisplayAlertAsync(title, message, "SI", "NO");
        }

        public async Task DisplayError(Exception ex)
        {
            if (IsBusy)
                IsBusy = false;

            //Sending stack trace to hockeyapp
            //await ReportService.PostReport(ex, false, "App Error", $"App Error: {ex.Message}");

            //Display general error
            await _dialogService?.DisplayAlertAsync("Error", "Ocurrió un error inesperado", "OK");

            //Showing error to console
            //await _dialogService?.DisplayAlertAsync("Error", ex.Message, "OK");
            Console.WriteLine(ex.ToString());
        }

        public async Task DisplayApiMessage<T>(ResponseResult<T> response)
        {
            if (IsBusy)
                IsBusy = false;

            if (response.StatusCode == 400)
            {
                //Display business error message
                await _dialogService?.DisplayAlertAsync("Error", response.Message, "OK");
            }
            else if (response.StatusCode == 500)
            {
                //Sending api reason error to hockeyapp
                //await ReportService.PostReport(new Exception(response.Message), false, "Api Error", $"API Error: {response.Message}");

                //Display general error
                await _dialogService?.DisplayAlertAsync("Error", "Ocurrió un error inesperado", "OK");

                //Showing error to console
                Console.WriteLine(response.Message);
            }
        }
    }
}
