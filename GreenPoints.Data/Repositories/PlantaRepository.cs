using GreenPoints.Domain;
using System.Linq;
using System.Collections.Generic;

namespace GreenPoints.Data
{
    public class PlantaRepository : IPlantaRepository
    {
        public List<Planta> Search()
        {
            using (var _context = new GreenPointsContext())
            {
                return _context.Plantas.Where(x => x.Activo).ToList();
            }
        }
    }
}
