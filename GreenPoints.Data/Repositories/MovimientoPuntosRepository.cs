using GreenPoints.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GreenPoints.Data
{
    public class MovimientoPuntosRepository : IMovimientoPuntosRepository
    {
        public void Create(MovimientoPuntos movimientoPuntos)
        {
            throw new NotImplementedException();
        }

        public List<MovimientoPuntos> GetBySocio(int socioId)
        {
            using (var _context = new GreenPointsContext())
            {
                return _context.Movimientos.Where(x => x.SocioId == socioId).ToList();
            }
        }
    }
}
