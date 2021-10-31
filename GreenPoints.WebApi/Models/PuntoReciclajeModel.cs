using System.Collections.Generic;

namespace GreenPoints.WebApi.Models
{
    public class PuntoReciclajeModel
    {
        public string Email { get; set; }
        public string CustomerName { get; set; }
        public string Document { get; set; }
        public decimal Longitud { get; set; }
        public decimal Latitud { get; set; }
        public string Direccion { get; set; }
        public string Password { get; set; }
        public List<int> Materials { get; set; }
    }
}
