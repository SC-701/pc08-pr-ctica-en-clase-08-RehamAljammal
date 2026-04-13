using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelo
{
    public class ProductoBase
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [RegularExpression(
            @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ0-9\s]+$",
            ErrorMessage = "El nombre solo puede contener letras, números y espacios"
        )]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria")]
        [RegularExpression(
            @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ0-9\s.,-]+$",
            ErrorMessage = "La descripción contiene caracteres no válidos"
        )]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio")]
        [RegularExpression(
            @"^\d+(\.\d{1,2})?$",
            ErrorMessage = "El precio debe ser un número válido con hasta 2 decimales"
        )]
        public string Precio { get; set; }

        [Required(ErrorMessage = "El stock es obligatorio")]
        [RegularExpression(
            @"^\d+$",
            ErrorMessage = "El stock debe ser un número entero positivo"
        )]
        public string Stock { get; set; }

        [Required(ErrorMessage = "El código de barras es obligatorio")]
        [RegularExpression(
            @"^\d{8,13}$",
            ErrorMessage = "El código de barras debe tener entre 8 y 13 dígitos"
        )]
        public string CodigoBarras { get; set; }
    }

    public class ProductoRequest : ProductoBase
    {
        public Guid IdSubCategoria { get; set; }
    }

    public class ProductoResponse : ProductoBase
    {
        public Guid Id { get; set; }
        public string SubCategoria { get; set; }
        public string Categoria { get; set; }

        //  aqui es donde está precio en USD
        public string PrecioUSD { get; set; }
    }
}
