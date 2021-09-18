using System;
using System.Collections.Generic;

namespace GreenPoints.Services
{
    public class IntercambioDto
    {
        public string PuntoReciclajeName { get; set; }
        public int Points { get; set; }
        public DateTime Date { get; set; }
        public List<IntercambioDetailDto> Detail { get; set; }
    }

    public class IntercambioDetailDto
    {
        public float Weight { get; set; }
        public string TipoReciclableName { get; set; }
    }
}
