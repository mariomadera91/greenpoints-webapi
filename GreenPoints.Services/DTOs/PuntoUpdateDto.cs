﻿using System.Collections.Generic;

namespace GreenPoints.Services
{
    public class PuntoUpdateDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public List<int> Materials { get; set; }
    }
}
