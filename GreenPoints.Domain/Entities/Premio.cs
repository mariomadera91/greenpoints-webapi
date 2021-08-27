﻿using System;

namespace GreenPoints.Domain
{
    public class Premio: ImageEntity
    {
        public string Descripcion { get; set; }
        public DateTime VigenciaDesde { get; set; }
        public DateTime VigenciaHasta { get; set; }
        public int Puntos { get; set; }
        public int Stock { get; set; }
        public DateTime? Fecha { get; set; }
        public string Observacion { get; set; }
        public int SponsorId { get; set; }
        public Sponsor Sponsor { get; set; }
    }
}
