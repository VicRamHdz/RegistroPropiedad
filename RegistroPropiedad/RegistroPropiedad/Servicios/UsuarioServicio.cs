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
    }
}
