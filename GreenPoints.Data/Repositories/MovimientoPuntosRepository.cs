using GreenPoints.Domain;
using System.Collections.Generic;
using System.Linq;

namespace GreenPoints.Data
{
    public class MovimientoPuntosRepository : IMovimientoPuntosRepository
    {
        public void Create(MovimientoPuntos movimientoPuntos)
        {
            using (var _context = new GreenPointsContext())
            {
                _context.Movimientos.Add(movimientoPuntos);
                _context.SaveChanges();
            }
        }

        public List<MovimientoPuntos> GetBySocio(int socioId)
        {
            using (var _context = new GreenPointsContext())
            {
                return _context.Movimientos.Where(x => x.SocioId == socioId).OrderByDescending(x => x.Fecha).ToList();
            }
        }
    }
}
