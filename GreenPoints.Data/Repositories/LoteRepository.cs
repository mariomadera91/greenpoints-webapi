using GreenPoints.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenPoints.Data
{
    public class LoteRepository : ILoteRepository
    {
        public List<Lote> GetActiveByPunto(int puntoId)
        {
            using (var _context = new GreenPointsContext())
            {
                return _context.Lotes.Where(x => x.PuntoId == puntoId && x.Abierto).ToList();
            }
        }

        public Lote GetActiveByTipoRecicable(int puntoId, int tipoId)
        {
            using (var _context = new GreenPointsContext())
            {
              return  _context.Lotes.Where(x => x.PuntoId == puntoId && x.TipoId == tipoId && x.Abierto).First();
            }
        }
    }
}
