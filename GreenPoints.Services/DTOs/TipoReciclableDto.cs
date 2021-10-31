using System;

namespace GreenPoints.Services
{
    public class TipoReciclableDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Points { get; set; }
        public string Imagen { get; set; }

        public Boolean Activo { get; set; }
        public bool HasActiveLote { get; set; }
    }
}
