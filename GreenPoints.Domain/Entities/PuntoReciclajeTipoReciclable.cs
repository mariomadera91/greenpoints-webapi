namespace GreenPoints.Domain
{
    public class PuntoReciclajeTipoReciclable: IdentifierEntity
    {
        public int PuntoId { get; set; }
        public int TipoId { get; set; }
        public PuntoReciclaje Punto { get; set; }
        public TipoReciclable Tipo { get; set; }
    }
}
