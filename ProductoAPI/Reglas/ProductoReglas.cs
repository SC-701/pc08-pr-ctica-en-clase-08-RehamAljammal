using Abstracciones.Interfaces.Reglas;
using Abstracciones.Interfaces.Servicios;
using System.Globalization;

namespace Reglas
{
    public class ProductoReglas : IProductoReglas
    {
        private readonly ITipoCambioServicio _tipoCambioServicio;

        public ProductoReglas(ITipoCambioServicio tipoCambioServicio)
        {
            _tipoCambioServicio = tipoCambioServicio;
        }

        public async Task<string> CalcularPrecioUSD(string precioCRC)
        {
            var tipoCambio = await _tipoCambioServicio.ObtenerTipoCambioVentaUSDAsync();
            return await CalcularPrecioUSD(precioCRC, tipoCambio);
        }

        
        public Task<string> CalcularPrecioUSD(string precioCRC, decimal tipoCambio)
        {
            if (string.IsNullOrWhiteSpace(precioCRC))
                return Task.FromResult("0.00");

            if (tipoCambio <= 0)
                throw new Exception("Tipo de cambio inválido.");

            if (!decimal.TryParse(precioCRC, NumberStyles.Number, CultureInfo.InvariantCulture, out var precio))
            {
                if (!decimal.TryParse(precioCRC, NumberStyles.Number, new CultureInfo("es-CR"), out precio))
                    throw new Exception("El precio no tiene un formato numérico válido.");
            }

            var usd = precio / tipoCambio;
            var resultado = Math.Round(usd, 2).ToString("0.00", CultureInfo.InvariantCulture);

            return Task.FromResult(resultado);
        }
    }
}
