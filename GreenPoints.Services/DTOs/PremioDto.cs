using System;

namespace GreenPoints.Services
{
    public class PremioDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? Desde { get; set; }
        public DateTime? Hasta { get; set; }
        public int Puntos { get; set; }
        public string Observacion { get; set; }
        public string Imagen { get; set; }
    }
}
