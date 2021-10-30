namespace GreenPoints.Domain
{
    public class PremioCodigo: StatusEntity
    {
        public string Codigo { get; set; }
        public int PremioId { get; set; }
        public Premio Premio { get; set; }
    }
}
