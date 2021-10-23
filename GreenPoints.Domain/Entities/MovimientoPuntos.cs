using System;

namespace GreenPoints.Domain
{
    public class MovimientoPuntos : IdentifierEntity
    {
        public int SocioId { get; set; }
        public TipoMovimiento Tipo { get; set; }
        public int Cantidad { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public SocioReciclador Socio { get; set; }
    }
}
