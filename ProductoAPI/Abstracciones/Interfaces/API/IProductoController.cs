using Abstracciones.Modelo;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.API
{
    public interface IProductoController
    {
        Task<IActionResult> Obtener();
        Task<IActionResult> Obtener(Guid Id);

        Task<IActionResult> Agregar(ProductoRequest producto);

        Task<IActionResult> Editar(Guid Id, ProductoRequest producto);

        Task<IActionResult> Eliminar(Guid Id);
    }
}
