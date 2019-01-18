using System;
using System.ComponentModel;
using RegistroPropiedad.Helper;

namespace RegistroPropiedad.Modelos
{
    public class TramitesModelo : ModeloBase
    {
        public string id { get; set; }
        public string sol_tipo_persona { get; set; }
        public string sol_tipo_doc { get; set; }
        public string sol_apellidos { get; set; }
        public string sol_nombres { get; set; }
        public string sol_cedula { get; set; }
        public string sol_provincia { get; set; }
        public string sol_ciudad { get; set; }
        public string sol_parroquia { get; set; }
        public string sol_calles { get; set; }
        public string sol_numero_casa { get; set; }
        public string sol_celular { get; set; }
        public string sol_convencional { get; set; }
        public string sol_correo { get; set; }
        public string fecha_solicitud { get; set; }
        public int tipo_solicitud { get; set; }
        public int motivo_solicitud { get; set; }
        public string otro_motivo { get; set; }
        public string numero_ficha { get; set; }
        public string clave_catastral { get; set; }
        public string tipo_inmueble { get; set; }
        public string prop_estado_civil { get; set; }
        public string prop_tipo_persona { get; set; }
        public string prop_tipo_doc { get; set; }
        public string prop_cedula { get; set; }
        public string prop_nombres { get; set; }
        public string prop_apellidos { get; set; }
        public string prop_conyugue_cedula { get; set; }
        public string prop_conyugue_nombres { get; set; }
        public string prop_conyugue_apellidos { get; set; }
        public string observacion { get; set; }
        public string ben_tipo_persona { get; set; }
        public string ben_tipo_doc { get; set; }
        public string ben_documento { get; set; }
        public string ben_nombres { get; set; }
        public string ben_apellidos { get; set; }
        public string ben_direccion { get; set; }
        public string ben_telefono { get; set; }
        public string ben_correo { get; set; }
        public string installmentsType { get; set; }
        public string payFrameUrl { get; set; }
        public int numeroTramite { get; set; }
        public long fechaIngeso { get; set; }
        public long fechaEntrega { get; set; }
        public long fechaInscripcion { get; set; }
        public bool sendEmail { get; set; }
        public string oid_document { get; set; }
        public double avance { get; set; }
        public string message { get; set; }
        public string image1 { get; set; }
        public string image2 { get; set; }
        public string image3 { get; set; }
        public string fechaSolicitudLong { get; set; }
        public string fechaInscripcionLong { get; set; }
        public string titulotramite { get { return $"# {numeroTramite} - { Enumerations.GetEnumDescription((TramiteDescripcion)motivo_solicitud)}"; } }
        public string propnombrecompleto { get { return $"{prop_nombres} {prop_apellidos}"; } }
        public string fechainicial { get { return fechaIngeso.ToDateTime().ToCustomDateTime(); } }
        public string fechainicialicon { get { return "iCalendario.svg"; } }
        public string fechafinal { get { return fechaEntrega.ToDateTime().ToCustomDateTime(); } }
        public string fechafinalicon { get { return fechaEntrega.ToDateTime() < DateTime.Now ? "iCalendarioFin.svg" : "iCalendarioProceso.svg"; } }
        public string motivosolicitudtitulo { get { return $"{Enumerations.GetEnumDescription((TramiteTipoSolicitud)tipo_solicitud)} {Enumerations.GetEnumDescription((TramiteDescripcion)motivo_solicitud)}"; } }
        public int porcentajeavance { get { return calcularPorcentaje(); } }

        private int calcularPorcentaje()
        {
            if (fechaEntrega.ToDateTime() < DateTime.Now)
            {
                return 100;
            }
            else
            {
                var diasTotales = int.Parse(Math.Round((fechaEntrega.ToDateTime() - fechaIngeso.ToDateTime()).TotalDays).ToString());
                var diasProgreso = int.Parse(Math.Round((DateTime.Now - fechaIngeso.ToDateTime()).TotalDays).ToString());
                return diasProgreso * 100 / diasTotales;
            }
        }
    }

    public enum TramiteDescripcion
    {
        [Description("TRÁMITE JUDICIAL")]
        TramiteJudicial = 1,
        [Description("TRÁMITE NOTARIAL")]
        TramiteNotarial = 2,
        [Description("TRÁMITE MUNICIPAL")]
        TramiteMunicipal = 3,
        [Description("TRÁMITE BIESS")]
        TramiteBIESS = 4,
        [Description("TRÁMITE PORTOAGUAS EP")]
        TramitePORTOAGUASEP = 5,
        [Description("TRÁMITE CNEL")]
        TramiteCNEL = 6,
        [Description("TRÁMITE OTROS(Especifique)")]
        TramiteOtros = 7
    }

    public enum TramiteTipoSolicitud
    {
        [Description("SOLICITUD DE CERTIFICADO DE HISTORIA DE DOMINIO")]
        HistoriaDominio = 1,
        [Description("SOLICITUD DE CERTIFICADO DE BÚQUEDA DE BIENES RAÍCES")]
        BusquedaBienesRaices = 2,
        [Description("SOLICITUD DE CERTIFICADO GENERAL")]
        General = 3
    }
}