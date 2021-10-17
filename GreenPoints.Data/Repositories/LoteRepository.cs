using GreenPoints.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace GreenPoints.Data
{
    public class LoteRepository : ILoteRepository
    {
        public void Create(Lote lote)
        {
            using (var _context = new GreenPointsContext())
            {
                _context.Lotes.Add(lote);
                _context.SaveChanges();
            }
        }

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

        public Lote GetById(int id)
        {
            using (var _context = new GreenPointsContext())
            {
                return _context.Lotes.Where(x => x.Id == id).FirstOrDefault();
            }
        }

        public List<Lote> GetByPunto(int puntoId)
        {
            using (var _context = new GreenPointsContext())
            {
                return _context.Lotes.Include(x=>x.Tipo)
                    .Where(x => x.PuntoId == puntoId).ToList();
            }
        }

        public void Update(Lote lote)
        {
            using (var _context = new GreenPointsContext())
            {
                _context.Update(lote);
                _context.SaveChanges();
            }
        }
    }
}
