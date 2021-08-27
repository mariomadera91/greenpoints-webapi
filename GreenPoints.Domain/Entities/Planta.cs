using System;

namespace GreenPoints.Domain
{
    public class Planta: ImageEntity
    {
        public string Nombre { get; set; }
        public DateTime FechaCrea { get; set; }
        public string Direccion { get; set; }
        public string Descripcion { get; set; }
    }
}
