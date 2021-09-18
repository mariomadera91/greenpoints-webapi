using System;
using System.Collections.Generic;

namespace GreenPoints.Domain
{
    public class Intercambio: IdentifierEntity
    {
        public int SocioId { get; set; }
        public int PuntoId { get; set; }
        public int Puntos { get; set; }
        public DateTime Fecha { get; set; }
        public SocioReciclador Socio { get; set; }
        public PuntoReciclaje Punto { get; set; }
        public List<IntercambioTipoReciclable> IntercambioTipoReciclables { get; set; }
    }
}
