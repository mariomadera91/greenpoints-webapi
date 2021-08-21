using System;

namespace GreenPoints.Domain.Entities
{
    public class Premio: BaseEntity
    {
        public string Descripcion { get; set; }
        public DateTime VigenciaDesde { get; set; }
        public DateTime VigenciaHasta { get; set; }
        public int Puntos { get; set; }
        public int Stock { get; set; }
        public DateTime? Fecha { get; set; }
        public string Observacion { get; set; }
        public bool Activo { get; set; }
        public string Imagen { get; set; }
    }
}
