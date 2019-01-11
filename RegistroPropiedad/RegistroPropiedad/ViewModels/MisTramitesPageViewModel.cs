using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Navigation;
using Prism.Services;
using RegistroPropiedad.Modelos;
using RegistroPropiedad.Servicios;
using Xamarin.Forms;

namespace RegistroPropiedad.ViewModels
{
    public class MisTramitesPageViewModel : VistaModeloBase
    {
        private ObservableCollection<TramitesModelo> _MisTramites;
        public ObservableCollection<TramitesModelo> MisTramites
        {
            get
            {
                return _MisTramites;
            }
            set
            {
                SetProperty(ref _MisTramites, value);
            }
        }

        public int Pagina { get; set; }

        private TramitesServicio _servicio;

        public ICommand VerTramiteCommand { get; set; }

        public MisTramitesPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
        {
            _navigation = navigationService;
            _dialogService = dialogService;
            VerTramiteCommand = new Command(async (p) => { await OnVerTramite(p as TramitesModelo); });
            _servicio = new TramitesServicio();
            Pagina = 0;
            CargarTramites(0);
        }

        public async Task CargarTramites(int paginacargar)
        {
            try
            {
                if (!await CheckInternet())
                {
                    return;
                }

                IsBusy = true;
                var response = await _servicio.CargarTramites(App.PerfilUsuarioInfo.id, paginacargar, 10);

                if (response.IsSuccess)
                {
                    if (response.Data != null && response.Data.Count > 0)
                        Pagina = paginacargar;

                    if (MisTramites == null)
                        MisTramites = new ObservableCollection<TramitesModelo>(response.Data);
                    else
                    {
                        foreach (var item in response.Data)
                        {
                            MisTramites.Add(item);
                        }
                    }
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

        private async Task OnVerTramite(TramitesModelo tramite)
        {
            try
            {
                NavigationParameters parametros = new NavigationParameters();
                parametros.Add("Tramite", tramite);
                await _navigation.NavigateAsync("TramiteDetallePage", parametros);
            }
            catch (Exception ex)
            {
                await DisplayError(ex);
            }
        }
    }
}
