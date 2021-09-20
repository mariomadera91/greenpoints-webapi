using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenPoints.Services
{
    public class CreatePuntoReciclajeDto
    {
        public string UserName { get; set; }
        public string CustomerName { get; set; }
        public string Document { get; set; }

        public string Direccion { get; set; }
        public string Password { get; set; }
    }
}
