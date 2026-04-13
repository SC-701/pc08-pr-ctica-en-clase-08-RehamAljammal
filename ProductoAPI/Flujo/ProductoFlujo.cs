using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Interfaces.Reglas;
using Abstracciones.Interfaces.Servicios;
using Abstracciones.Modelo;

namespace Flujo
{
    public class ProductoFlujo : IProductoFlujo
    {
        private readonly IProductoDA _productoDA;
        private readonly IProductoReglas _productoReglas;
        private readonly ITipoCambioServicio _tipoCambioServicio;

        public ProductoFlujo(IProductoDA productoDA, IProductoReglas productoReglas, ITipoCambioServicio tipoCambioServicio)
        {
            _productoDA = productoDA;
            _productoReglas = productoReglas;
            _tipoCambioServicio = tipoCambioServicio;
        }

        public Task<Guid> Agregar(ProductoRequest producto) => _productoDA.Agregar(producto);

        public Task<Guid> Editar(Guid Id, ProductoRequest producto) => _productoDA.Editar(Id, producto);

        public Task<Guid> Eliminar(Guid Id) => _productoDA.Eliminar(Id);

        // ✅ Listado: 1 sola llamada al BCCR
        public async Task<IEnumerable<ProductoResponse>> Obtener()
        {
            var productos = (await _productoDA.Obtener())?.ToList();
            if (productos == null || productos.Count == 0)
                return productos;

            var tipoCambio = await _tipoCambioServicio.ObtenerTipoCambioVentaUSDAsync();

            foreach (var p in productos)
            {
                p.PrecioUSD = await _productoReglas.CalcularPrecioUSD(p.Precio, tipoCambio);
            }

            return productos;
        }

        public async Task<ProductoResponse> Obtener(Guid Id)
        {
            var producto = await _productoDA.Obtener(Id);
            if (producto == null) return null;

            producto.PrecioUSD = await _productoReglas.CalcularPrecioUSD(producto.Precio);
            return producto;
        }
    }
}
