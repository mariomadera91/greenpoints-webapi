using System;

namespace GreenPoints.Domain
{
    public class Intercambio: IdentifierEntity
    {
        public int SocioId { get; set; }
        public int PuntoId { get; set; }
        public DateTime Fecha { get; set; }
        public SocioReciclador Socio { get; set; }
        public PuntoReciclaje Punto { get; set; }
    }
}
