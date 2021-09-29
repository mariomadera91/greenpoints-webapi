using System;
using System.Collections.Generic;

namespace GreenPoints.Domain
{
    public class IntercambioTipoReciclable: IdentifierEntity
    {
        public int IntercambioId { get; set; }
        public int TipoId { get; set; }
        public float Peso { get; set; }
        public int Puntos { get; set; }
        public int LoteId { get; set; }
        public TipoReciclable Tipo { get; set; }
        public Intercambio Intercambio { get; set; }
        public Lote Lote { get; set; }

        public static implicit operator List<object>(IntercambioTipoReciclable v)
        {
            throw new NotImplementedException();
        }
    }
}
