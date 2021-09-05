using GreenPoints.Domain;
using System.Collections.Generic;
using System.Linq;

namespace GreenPoints.Data
{
    public class PremioRepository : IPremioRepository
    {
        public List<Premio> Get()
        {
            using (var _context = new GreenPointsContext())
            {
                return _context.Premios.ToList();
            }
        }

        public Premio GetById(int id)
        {
            using (var _context = new GreenPointsContext())
            {
                return _context.Premios.Where(x => x.Id == id).FirstOrDefault();
            }
        }
    }
}
