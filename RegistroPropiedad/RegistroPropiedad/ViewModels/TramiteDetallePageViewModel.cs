using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Navigation;
using Prism.Services;
using RegistroPropiedad.Modelos;
using Xamarin.Forms;

namespace RegistroPropiedad.ViewModels
{
    public class TramiteDetallePageViewModel : VistaModeloBase
    {
        public ICommand RegresarCommand { get; set; }

        private TramitesModelo _Model;
        public TramitesModelo Model
        {
            get { return _Model; }
            set
            {
                SetProperty(ref _Model, value);
            }
        }

        public TramiteDetallePageViewModel(INavigationService navigationService, IPageDialogService dialogService)
        {
            _navigation = navigationService;
            _dialogService = dialogService;
            RegresarCommand = new Command(async () => { await OnRegresar(); });
            Title = "Estado del Trámite";
        }

        private async Task OnRegresar()
        {
            await _navigation.GoBackAsync();
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            if (parameters.ContainsKey("Tramite"))
            {
                Model = (TramitesModelo)parameters["Tramite"];
                RaisePropertyChanged(nameof(Model));
                MessagingCenter.Send<string>("Grafica", "CalcularGrafica");
            }
        }
    }
}
