using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Navigation;
using Prism.Services;
using RegistroPropiedad.Modelos;
using RegistroPropiedad.Servicios;
using Xamarin.Forms;

namespace RegistroPropiedad.ViewModels
{
    public class NuevoTramitePagoPageViewModel : VistaModeloBase
    {
        public ICommand FacturaSolicitanteCommand { get; set; }

        private bool _FacturaSolicitante;
        public bool FacturaSolicitante
        {
            get { return _FacturaSolicitante; }
            set
            {
                SetProperty(ref _FacturaSolicitante, value);
            }
        }

        private bool _NoFacturaSolicitante;
        public bool NoFacturaSolicitante
        {
            get { return _NoFacturaSolicitante; }
            set
            {
                SetProperty(ref _NoFacturaSolicitante, value);
            }
        }

        static Dictionary<string, int> motivos { get; } = new Dictionary<string, int>
        {
            { "Trámite Judicial", 1 },
            { "Trámite Notarial", 2 },
            { "Trámite Municipal", 3 },
            { "Trámite BIESS", 4 },
            { "Trámite PORTOAGUAS EP", 5 },
            { "Trámite CNEL", 6 },
            { "Otros (Especifique)", 7 },
        };

        public List<string> Motivos { get; } = motivos.Keys.ToList();

        public ICommand RegresarCommand { get; set; }
        public ICommand BuscarCedulaCommand { get; set; }
        public ICommand RegistrarCommand { get; set; }

        public TramiteDetalleModelo Modelo { get; set; }
        private TramitesServicio _servicio;

        public NuevoTramitePagoPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
        {
            _navigation = navigationService;
            _dialogService = dialogService;
            FacturaSolicitanteCommand = new Command(async () => { await OnFacturaSolicitante(); });
            Title = "Nuevo Trámite";
            _servicio = new TramitesServicio();
            FacturaSolicitante = true;
            Modelo = new TramiteDetalleModelo();
            BuscarCedulaCommand = new Command(async () => { await OnBuscarCedula(); });
            RegresarCommand = new Command(async () => { await OnRegresar(); });
            RegistrarCommand = new Command(async () => { await OnRegistrar(); });
        }

        private async Task OnFacturaSolicitante()
        {
            try
            {
                await Task.Run(() =>
                {
                    FacturaSolicitante = !FacturaSolicitante;
                    NoFacturaSolicitante = !FacturaSolicitante;
                    Modelo.Nombres = string.Empty;
                    Modelo.Apellidos = string.Empty;
                    Modelo.Direccion = string.Empty;
                    Modelo.CorreoElectronico = string.Empty;
                    Modelo.EstadoCivil = 0;
                    Modelo.Celular = string.Empty;
                });
            }
            catch (Exception ex)
            {
                await DisplayError(ex);
            }
        }

        private async Task OnBuscarCedula()
        {
            try
            {
                if (!await CheckInternet())
                {
                    return;
                }

                IsBusy = true;
                var response = await _servicio.BuscarCedula(Modelo.Cedula);

                if (response.IsSuccess)
                {
                    Modelo.Nombres = response.Data.nombres;
                    Modelo.Apellidos = response.Data.apellidos;
                    Modelo.Direccion = response.Data.direccion;
                    Modelo.CorreoElectronico = response.Data.correo1;
                    Modelo.EstadoCivil = response.Data.estadoCivil;
                    Modelo.Celular = response.Data.telefono1;
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

        private async Task OnRegistrar()
        {
            try
            {

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
