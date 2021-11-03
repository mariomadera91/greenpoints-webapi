using System;

namespace GreenPoints.Services
{
    public class SponsorDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; } = true;
        public string Imagen { get; set; }
        public ImagenDto ImageData { get; set; }
    }
}
