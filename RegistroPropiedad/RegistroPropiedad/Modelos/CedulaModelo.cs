using System;
namespace RegistroPropiedad.Modelos
{
    public class CedulaModelo : ModeloBase
    {
        public int id { get; set; }
        public string cedRuc { get; set; }
        private string _nombres;
        public string nombres
        {
            get { return _nombres; }
            set { SetProperty(ref _nombres, value); }
        }
        private string _apellidos;
        public string apellidos
        {
            get { return _apellidos; }
            set { SetProperty(ref _apellidos, value); }
        }
        public string tipoDocumento { get; set; }
        public string tipoPersona { get; set; }
        public long fechaCreacion { get; set; }
        public long fechaNacimiento { get; set; }
        public string direccion { get; set; }
        public string telefono1 { get; set; }
        public string telefono2 { get; set; }
        private string _correo1;
        public string correo1
        {
            get { return _correo1; }
            set { SetProperty(ref _correo1, value); }
        }
        public string correo2 { get; set; }
        public bool estado { get; set; }
        public int estadoCivil { get; set; }
        public string genero { get; set; }
        public int discapacidad { get; set; }
        public int derecho { get; set; }
        public int alcance { get; set; }
        public int tipoPersonaWs { get; set; }
        public string numeroCasa { get; set; }
    }
}
