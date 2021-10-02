using System;
using System.Collections.Generic;

namespace GreenPoints.Services
{
    public class IntercambioDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PuntoReciclajeName { get; set; }
        public string PuntoReciclajeAddress { get; set; }
        public int Points { get; set; }
        public DateTime Date { get; set; }
        public List<IntercambioDetailDto> Detail { get; set; }
    }

    public class IntercambioDetailDto
    {
        public float Weight { get; set; }
        public string TipoReciclableName { get; set; }
        public string PlantaName { get; set; }
        public string PlantaAddress { get; set; }
        public bool ClosedLote { get; set; }
    }
}
