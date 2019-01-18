using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RegistroPropiedad.Modelos;

namespace RegistroPropiedad.Servicios
{
    public class TramitesServicio : ServicioBase
    {
        public async Task<ResponseResult<List<TramitesModelo>>> CargarTramites(int idusuario, int pagina, int tamaniopagina = 10)
        {
            //var endpoint = $"consulta/usuario/solicitud?userId=6&page=0&size=10";
            var endpoint = $"consulta/usuario/solicitud?userId={idusuario}&page={pagina}&size={tamaniopagina}";
            var result = await GetAsync<List<TramitesModelo>>(endpoint);
            return result;
        }

        public async Task<ResponseResult<CedulaModelo>> BuscarCedula(string cedula)
        {
            var endpoint = $"registro/persona/cedula/{cedula}";
            var result = await GetAsync<CedulaModelo>(endpoint);
            return result;
        }
    }
}
