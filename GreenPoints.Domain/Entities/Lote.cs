using System;

namespace GreenPoints.Domain
{
    public class Lote: IdentifierEntity
    {
        public int PuntoId { get; set; }
        public int TipoId { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaCierre { get; set; }
        public bool Abierto { get; set; }
        public int PlantaId { get; set; }
        public TipoReciclable Tipo { get; set; }
        public Planta Planta { get; set; }
    }
}
