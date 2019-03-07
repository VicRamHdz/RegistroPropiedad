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
    public class NuevoTramitePageViewModel : VistaModeloBase
    {
        static Dictionary<string, int> tiposTramite { get; } = new Dictionary<string, int>
        {
            { "Historia de Dominio", 1 },
            { "Bienes Raíces", 2 },
            { "Certificado General", 3 }
        };

        public List<string> TiposTramite { get; } = tiposTramite.Keys.ToList();

        private string _TipoTramiteSeleccionado;
        public string TipoTramiteSeleccionado
        {
            get { return _TipoTramiteSeleccionado; }
            set
            {
                _TipoTramiteSeleccionado = value;
                if (string.IsNullOrEmpty(_TipoTramiteSeleccionado)) return;
                Modelo.TipoCertificado = tiposTramite[_TipoTramiteSeleccionado];
            }
        }

        static Dictionary<string, int> tiposPersona { get; } = new Dictionary<string, int>
        {
            { "Natural", 1 },
            { "Juridica", 2 }
        };

        public List<string> TiposPersona { get; } = tiposPersona.Keys.ToList();

        private string _TipoPersonaSeleccionado;
        public string TipoPersonaSeleccionado
        {
            get { return _TipoPersonaSeleccionado; }
            set
            {
                SetProperty(ref _TipoPersonaSeleccionado, value);
                if (string.IsNullOrEmpty(_TipoPersonaSeleccionado)) return;
                Modelo.TipoPersona = tiposPersona[_TipoPersonaSeleccionado];
                if (Modelo.TipoPersona == 1)
                {
                    EsPersonaNatural = true;
                    EsPersonaJuridica = false;
                }
                else if (Modelo.TipoPersona == 2)
                {
                    EsPersonaNatural = false;
                    EsPersonaJuridica = true;
                }
            }
        }

        private bool _EsPersonaJuridica;
        public bool EsPersonaJuridica
        {
            get { return _EsPersonaJuridica; }
            set
            {
                SetProperty(ref _EsPersonaJuridica, value);
            }
        }

        private bool _EsPersonaNatural;
        public bool EsPersonaNatural
        {
            get { return _EsPersonaNatural; }
            set
            {
                SetProperty(ref _EsPersonaNatural, value);
            }
        }

        static Dictionary<string, int> estadoCivil { get; } = new Dictionary<string, int>
        {
            { "Soltero", 1 },
            { "Casado", 2 },
            { "Divorciado", 3 },
            { "Unión Libre", 4 },
            { "Viudo", 5 },
        };

        public List<string> EstadoCivil { get; } = estadoCivil.Keys.ToList();

        static Dictionary<string, int> tipoInmueble { get; } = new Dictionary<string, int>
        {
            { "Casa", 1 },
            { "Terreno", 2 },
            { "Departamento", 3 },
            { "Bodega", 4 },
            { "Local / Almacén", 5 },
            { "Parqueadero", 6 },
            { "Otros", 7 },
        };

        public List<string> TipoInmueble { get; } = tipoInmueble.Keys.ToList();

        public ICommand SiguienteCommand { get; set; }
        public ICommand BuscarCedulaCommand { get; set; }
        public ICommand TomarImagen1Command { get; set; }
        public ICommand TomarImagen2Command { get; set; }
        public ICommand TomarImagen3Command { get; set; }

        public TramiteDetalleModelo Modelo { get; set; }
        private TramitesServicio _servicio;

        public NuevoTramitePageViewModel(INavigationService navigationService, IPageDialogService dialogService)
        {
            _navigation = navigationService;
            _dialogService = dialogService;
            Modelo = new TramiteDetalleModelo();
            TipoPersonaSeleccionado = "Natural";
            _servicio = new TramitesServicio();
            SiguienteCommand = new Command(async (p) => { await OnSiguiente(); });
            BuscarCedulaCommand = new Command(async () => { await OnBuscarCedula(); });
            TomarImagen1Command = new Command(() => { OnTomarImagen1(); });
            TomarImagen2Command = new Command(() => { OnTomarImagen2(); });
            TomarImagen3Command = new Command(() => { OnTomarImagen3(); });
            CargarCombos();
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

        private void OnTomarImagen1()
        {
            MessagingCenter.Send<string>("Foto", "TomarFoto1");
        }

        private void OnTomarImagen2()
        {
            MessagingCenter.Send<string>("Foto", "TomarFoto2");
        }

        private void OnTomarImagen3()
        {
            MessagingCenter.Send<string>("Foto", "TomarFoto3");
        }

        private async Task OnSiguiente()
        {
            try
            {
                await _navigation.NavigateAsync("NuevoTramitePagoPage");
            }
            catch (Exception ex)
            {
                await DisplayError(ex);
            }
        }

        private void CargarCombos()
        {
        }
    }
}