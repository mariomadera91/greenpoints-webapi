using System.Collections.Generic;

namespace GreenPoints.Services
{
    public class CreatePremioDto
    {
        public string Nombre { get; set; }
        public string Sponsor { get; set; }
        public string FechaInicio { get; set; }
        public string FechaVto { get; set; }
        public string Descripcion { get; set; }
        public string Observacion { get; set; }
        public int Puntos { get; set; }
        public List<string> Codigos { get; set; }
        public ImagenDto Image { get; set; }
    }
}
