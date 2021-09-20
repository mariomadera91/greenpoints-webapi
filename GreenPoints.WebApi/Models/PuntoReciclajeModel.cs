using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenPoints.WebApi.Models
{
    public class PuntoReciclajeModel
    {
        public string Username { get; set; }
        public string CustomerName { get; set; }
        public string Document { get; set; }
        public string Direccion { get; set; }

        public string Password { get; set; }
    }
}
