using System;
namespace RegistroPropiedad.Modelos
{
    public class TramiteDetalleModelo : ModeloBase
    {
        public int TipoCertificado { get; set; }
        public int TipoPersona { get; set; }
        public string Cedula { get; set; }

        private string _Nombres;
        public string Nombres
        {
            get { return _Nombres; }
            set { SetProperty(ref _Nombres, value); }
        }

        private string _Apellidos;
        public string Apellidos
        {
            get { return _Apellidos; }
            set { SetProperty(ref _Apellidos, value); }
        }

        private string _Direccion;
        public string Direccion
        {
            get { return _Direccion; }
            set { SetProperty(ref _Direccion, value); }
        }

        private string _CorreoElectronico;
        public string CorreoElectronico
        {
            get { return _CorreoElectronico; }
            set { SetProperty(ref _CorreoElectronico, value); }
        }

        private string _Celular;
        public string Celular
        {
            get { return _Celular; }
            set { SetProperty(ref _Celular, value); }
        }

        private int _EstadoCivil;
        public int EstadoCivil
        {
            get { return _EstadoCivil; }
            set { SetProperty(ref _EstadoCivil, value); }
        }

        private byte _Imagen1;
        public byte Imagen1
        {
            get { return _Imagen1; }
            set { SetProperty(ref _Imagen1, value); }
        }

        private byte _Imagen2;
        public byte Imagen2
        {
            get { return _Imagen2; }
            set { SetProperty(ref _Imagen2, value); }
        }

        private byte _Imagen3;
        public byte Imagen3
        {
            get { return _Imagen3; }
            set { SetProperty(ref _Imagen3, value); }
        }
    }
}
