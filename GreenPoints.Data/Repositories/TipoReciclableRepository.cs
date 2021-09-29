using GreenPoints.Domain;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GreenPoints.Data
{
    public class TipoReciclableRepository : ITipoReciclableRepository
    {
        public List<TipoReciclable> Get()
        {
            using (var _context = new GreenPointsContext())
            {
                return _context.TiposReciclables.Where(x => x.Activo).ToList();
            }
        }

        public List<TipoReciclable> GetByPunto(int puntoId)
        {
            using (var _context = new GreenPointsContext())
            {
                return _context.PuntosReciclajeTiposReciclables
                        .Include(x => x.Punto)
                        .Include(x => x.Tipo)
                        .Where(x => x.Punto.Id == puntoId ).Select(x => x.Tipo).ToList();
            }
        }
    }
}
