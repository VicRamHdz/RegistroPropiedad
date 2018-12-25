using System;
namespace RegistroPropiedad.Modelos
{
    public class PerfilUsuarioModelo : ModeloBase
    {
        public int id { get; set; }
        public string username { get; set; }
        public string pass { get; set; }
        public bool habilitado { get; set; }
        public Persona persona { get; set; }
        public string token { get; set; }
        public string personaID { get; set; }
    }

    public class Persona : ModeloBase
    {
        public int id { get; set; }
        public string cedRuc { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string tipoDocumento { get; set; }
        public string tipoPersona { get; set; }
        public string fechaCreacion { get; set; }
        public string fechaNacimiento { get; set; }
        public string direccion { get; set; }
        public string telefono1 { get; set; }
        public string telefono2 { get; set; }
        public string correo1 { get; set; }
        public string correo2 { get; set; }
        public bool estado { get; set; }
        public string estadoCivil { get; set; }
        public string genero { get; set; }
        public string discapacidad { get; set; }
        public string derecho { get; set; }
        public string alcance { get; set; }
        public string tipoPersonaWs { get; set; }
        public string numeroCasa { get; set; }
    }
}
