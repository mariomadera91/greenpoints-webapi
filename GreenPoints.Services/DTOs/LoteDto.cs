using System;

namespace GreenPoints.Services
{
    public class LoteDto
    {
        public int Id { get; set; }
        public string TipoMaterialNombre { get; set; }
        public string PlantaNombre { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaCierre { get; set; }
        public string Imagen { get; set; }
        public bool Abierto { get; set; }
        public string Kilos { get; set; }
    }
}
