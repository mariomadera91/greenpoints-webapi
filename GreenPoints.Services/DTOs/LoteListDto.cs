using System;

namespace GreenPoints.Services
{
    public class LoteListDto
    {
        public int Id { get; set; }
        public string TipoMaterialNombre { get; set; }
        public DateTime? Fecha { get; set; }
        public string Imagen { get; set; }
        public bool Abierto { get; set; }
        public decimal Kilos { get; set; }
    }
}
