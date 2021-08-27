namespace GreenPoints.Domain
{
    public class SocioReciclador: ImageEntity
    {
        public string Mail { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string FechaNac { get; set; }
        public string Password { get; set; }
    }
}
