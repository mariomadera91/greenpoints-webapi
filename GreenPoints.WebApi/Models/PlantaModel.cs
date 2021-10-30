using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenPoints.WebApi.Models
{
    public class PlantaModel
    {
        public string Nombre { get; set; }
        public DateTime FechaCrea { get; set; }

        public string Direccion { get; set; }
        public string Descripcion { get; set; }
    }
}
