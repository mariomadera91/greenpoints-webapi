using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenPoints.Services
{
    public class SocioPremioDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime? Hasta { get; set; }
        public int Codigo { get; set; }
        public string Imagen { get; set; }
    }
}
