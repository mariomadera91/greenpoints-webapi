using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenPoints.WebApi.Models
{
    public class IntercambioModel
    {
        public int PuntoId { get; set; }
        public int SocioId { get; set; }

        public List<IntercambioTipoMateriales> TipoReciclaje { get; set; }
    }

    public class IntercambioTipoMateriales
    {
        public int TipoId { get; set; }
        public decimal Peso { get; set; }
        public int Puntos { get; set; }
    }
}
