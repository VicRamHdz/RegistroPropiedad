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
    }
}
