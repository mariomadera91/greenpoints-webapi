using System;

namespace GreenPoints.Services
{
    public class MovimientoPuntosDto
    {
        public string Tipo { get; set; }
        public string Cantidad { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public bool Aumentar { get; set; }
    }
}
