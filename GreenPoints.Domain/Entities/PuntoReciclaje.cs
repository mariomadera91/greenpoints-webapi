namespace GreenPoints.Domain
{
    public class PuntoReciclaje : IdentifierEntity
    {
        public string Nombre { get; set; }
        public string CUIT { get; set; }
        public string Direccion { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
    }
}
