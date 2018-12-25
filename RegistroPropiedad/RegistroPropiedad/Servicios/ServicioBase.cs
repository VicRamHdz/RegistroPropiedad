using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RegistroPropiedad.Modelos;

namespace RegistroPropiedad.Servicios
{
    public abstract class ServicioBase
    {
        #region Private Members
        private HttpClient _client;
        private string _endpointUrl;
        #endregion

        #region Private Methods
        private Uri UriBuilder(string endpoint) => new Uri(string.Concat(_endpointUrl, endpoint));

        #endregion

        #region Protected Methods
        protected ServicioBase()
        {
            _client = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(300),
            };

            var byteArray = Encoding.ASCII.GetBytes($"{Constantes.UsuarioServicio}:{Constantes.PwdServicio}");
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            _endpointUrl = Constantes.BaseApiUrl;
        }

        protected async Task<ResponseResult<T>> GetAsync<T>(string endpoint, CancellationToken token = new CancellationToken())
        {
            var res = new ResponseResult<T>();
            HttpResponseMessage response = new HttpResponseMessage();
            var url = UriBuilder(endpoint);

            try
            {
                response = await _client.GetAsync(url, token);
                res = await GettingStandardResponse<T>(response);
            }
            catch (Exception ex)
            {
                ex.InnerException.Source = $"Error parsing: \n Type: {nameof(T)} \n At method: GetAsync \n URL: {UriBuilder(endpoint)} \n Json Response: {response.ReasonPhrase}";
                throw ex;
            }

            return res;
        }

        protected async Task<ResponseResult<T>> PostAsync<R, T>(string endpoint, R requestBody)
        {
            var json = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            return await PostAsync<T>(endpoint, content);
        }

        protected async Task<ResponseResult<T>> PostAsync<T>(string endpoint, HttpContent content)
        {
            var res = new ResponseResult<T>();
            HttpResponseMessage response = new HttpResponseMessage();
            var url = UriBuilder(endpoint);

            try
            {
                response = await _client.PostAsync(url, content);
                res = await GettingStandardResponse<T>(response);
            }
            catch (Exception ex)
            {
                ex.Data.Add("ErrorInfo", $"Error parsing: \n Type: {nameof(T)} \n At method: PostAsync \n URL: {UriBuilder(endpoint)} \n Json Response: {response.ReasonPhrase}");
                throw ex;
            }

            return res;

        }

        protected async Task<ResponseResult<T>> PutAsync<R, T>(string endpoint, R requestBody)
        {
            var json = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            return await PutAsync<T>(endpoint, content);
        }

        protected async Task<ResponseResult<T>> PutAsync<T>(string endpoint, HttpContent content)
        {
            var res = new ResponseResult<T>();
            HttpResponseMessage response = new HttpResponseMessage();
            var url = UriBuilder(endpoint);

            try
            {
                response = await _client.PutAsync(url, content);
                res = await GettingStandardResponse<T>(response);
            }
            catch (Exception ex)
            {
                ex.Data.Add("ErrorInfo", $"Error parsing: \n Type: {nameof(T)} \n At method: PutAsync \n URL: {UriBuilder(endpoint)} \n Json Response: {response.ReasonPhrase}");
                throw ex;
            }

            return res;

        }

        private async Task<ResponseResult<T>> GettingStandardResponse<T>(HttpResponseMessage response)
        {
            var res = new ResponseResult<T>();
            if (response.IsSuccessStatusCode)
            {
                res.StatusCode = 200;
                res.Status = "Success";
                res.Message = "Call success";
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };
                var responseContent = await response.Content.ReadAsStringAsync();
                res.Data = JsonConvert.DeserializeObject<T>(responseContent, settings);
            }
            else if (response.StatusCode >= System.Net.HttpStatusCode.BadRequest && response.StatusCode < System.Net.HttpStatusCode.InternalServerError)
            {
                res.StatusCode = 400;
                res.Status = "BadRequest";
                res.Data = default(T);
                var badResponseContent = await response.Content.ReadAsStringAsync();
                res.Message = JsonConvert.DeserializeObject<BadRequestResponseResult>(badResponseContent).Message;
            }
            else if (response.StatusCode >= System.Net.HttpStatusCode.InternalServerError)
            {
                res.StatusCode = 500;
                res.Status = "InternalServerError";
                res.Data = default(T);
                //res.Message = response.ReasonPhrase;
                res.Message = await response.Content.ReadAsStringAsync();
            }
            return res;
        }

        #endregion

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}
