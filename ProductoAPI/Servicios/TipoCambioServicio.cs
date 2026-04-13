
using Abstracciones.Interfaces.Reglas;
using Abstracciones.Interfaces.Servicios;
using Abstracciones.Modelo.Servicios.BancoCentral;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Servicios
{
    public class TipoCambioServicio : ITipoCambioServicio
    {
        private readonly IConfiguracion _configuracion;
        private readonly IHttpClientFactory _httpClient;

        public TipoCambioServicio(IConfiguracion configuracion, IHttpClientFactory httpClient)
        {
            _configuracion = configuracion;
            _httpClient = httpClient;
        }

        public async Task<decimal> ObtenerTipoCambioVentaUSDAsync()
        {
            var urlBase = _configuracion.ObtenerValor("BancoCentralCR:UrlBase");
            var token = _configuracion.ObtenerValor("BancoCentralCR:BearerToken");

            var fecha = DateTime.Now.ToString("yyyy/MM/dd");
            var url = $"{urlBase}?fechaInicio={fecha}&fechaFin={fecha}&idioma=ES";

            var cliente = _httpClient.CreateClient("BancoCentralCR");
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var respuesta = await cliente.GetAsync(url);
            respuesta.EnsureSuccessStatusCode();

            var json = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var data = JsonSerializer.Deserialize<BccrResponse>(json, opciones);

            var tipoCambio = data?
                .Datos?.FirstOrDefault()?
                .Indicadores?.FirstOrDefault()?
                .Series?.FirstOrDefault()?
                .ValorDatoPorPeriodo;

            if (tipoCambio == null || tipoCambio <= 0)
                throw new Exception("No se pudo obtener el tipo de cambio del BCCR para la fecha actual.");

            return tipoCambio.Value;
        }
    }
}
