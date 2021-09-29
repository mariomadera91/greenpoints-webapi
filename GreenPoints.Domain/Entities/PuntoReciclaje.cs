using System.Collections.Generic;

namespace GreenPoints.Domain
{
    public class PuntoReciclaje : IdentifierEntity
    {
        public string Nombre { get; set; }
        public string CUIT { get; set; }
        public string Direccion { get; set; }
        public decimal Longitud { get; set; }
        public decimal Latitud { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public List<PuntoReciclajeTipoReciclable> PuntoReciclajeTipoReciclables { get; set; }
    }
}
