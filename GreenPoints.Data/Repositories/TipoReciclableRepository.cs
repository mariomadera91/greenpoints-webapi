using GreenPoints.Domain;
using System.Linq;
using System.Collections.Generic;

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
    }
}
