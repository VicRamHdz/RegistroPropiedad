using System;
using System.Threading.Tasks;
using RegistroPropiedad.Modelos;

namespace RegistroPropiedad.Servicios
{
    public class UsuarioServicio : ServicioBase
    {
        public async Task<ResponseResult<PerfilUsuarioModelo>> IniciarSesion(string identificacion, string contra)
        {
            var endpoint = $"user/{identificacion}/pass/{contra}";
            var result = await GetAsync<PerfilUsuarioModelo>(endpoint);
            return result;
        }

        public async Task<ResponseResult<PerfilUsuarioModelo>> ValidarExistenciaCedula(string cedula)
        {
            var endpoint = $"usuario/{cedula}";
            var result = await GetAsync<PerfilUsuarioModelo>(endpoint);
            return result;
        }

        public async Task<ResponseResult<string>> CrearPerfilUsuario(PerfilUsuarioModelo perfil)
        {
            var endpoint = $"registro/usuario";
            var result = await PostAsync<PerfilUsuarioModelo, string>(endpoint, perfil);
            return result;
        }

        public async Task<ResponseResult<string>> RecuperarContra(string email, string nombreusuario, int id)
        {
            var endpoint = $"usuario/recuperar/{email}/{nombreusuario}/{id}";
            var p = new { };
            var result = await PostAsync<object, string>(endpoint, p);
            return result;
        }
        //http://18.215.235.24:8952/rppconsultas/api/usuario/recuperar/andysanchezgonzalez1996@gmail.com/0953255957/90
    }
}
