using GreenPoints.Domain;
using System.Collections.Generic;
using System.Linq;

namespace GreenPoints.Data
{
    public class PremioRepository : IPremioRepository
    {
        public GreenPointsContext _context { get; set; }

        public PremioRepository()
        {
            _context = new GreenPointsContext();
        }

        public List<Premio> Get()
        {
            return _context.Premios.ToList();
        }

        public Premio GetById(int id)
        {
            return _context.Premios.Where(x => x.Id == id).FirstOrDefault();
        }
    }
}
