using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenPoints.Services
{
    public class CreateIntercambioDto
    {
        public int PuntoId { get; set; }
        public int SocioId { get; set; }

        public List<CreateIntercambioTipoMateriales> TipoReciclaje { get; set; }

    }

    public class CreateIntercambioTipoMateriales
    {
        public int TipoId { get; set; }
        public float Peso { get; set; }
        public int Puntos { get; set; }
    }
}
