using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelo.Servicios.BancoCentral
{
    public class BccrResponse
    {
        public bool Estado { get; set; }
        public string Mensaje { get; set; }
        public List<BccrDato> Datos { get; set; }
    }

    public class BccrDato
    {
        public string Titulo { get; set; }
        public string Periodicidad { get; set; }
        public List<BccrIndicador> Indicadores { get; set; }
    }

    public class BccrIndicador
    {
        public string CodigoIndicador { get; set; }
        public string NombreIndicador { get; set; }
        public List<BccrSerie> Series { get; set; }
    }

    public class BccrSerie
    {
        public string Fecha { get; set; }
        public decimal ValorDatoPorPeriodo { get; set; }
    }
}

