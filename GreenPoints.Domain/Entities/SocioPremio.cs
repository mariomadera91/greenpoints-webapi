using System;

namespace GreenPoints.Domain
{
    public class SocioPremio: IdentifierEntity
    {
        public int SocioId { get; set; }
        public int PremioId { get; set; }
        public DateTime Fecha { get; set; }
        public int CodigoId { get; set; }
        public SocioReciclador Socio { get; set; }
        public Premio Premio { get; set; }
        public PremioCodigo Codigo { get; set; }
    }
}
