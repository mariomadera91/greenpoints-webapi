namespace GreenPoints.Services
{
    public class CreateDonacionDto
    {
        public int SocioOrigenId { get; set; }
        public int SocioDestinoId { get; set; }
        public int Puntos { get; set; }
    }
}
