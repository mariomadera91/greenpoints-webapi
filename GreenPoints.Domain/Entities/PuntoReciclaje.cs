namespace GreenPoints.Domain
{
    public class PuntoReciclaje: ImageEntity
    {
        public string Nombre { get; set; }
        public string CUIT { get; set; }
        public string Usuario { get; set; }
        public string Password { get; set; }
        public string Direccion { get; set; }
    }
}
