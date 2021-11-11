using GreenPoints.Domain;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GreenPoints.Data
{
    public class IntercambioRepository : IIntercambioRepository
    {
        public void Create(Intercambio intercambio)
        {
            using (var _context = new GreenPointsContext())
            {
                 _context.Intercambios.Add(intercambio);
                 _context.SaveChanges();
            }
        }

        public Intercambio GetById(int id)
        {
            using (var _context = new GreenPointsContext())
            {
                return _context.Intercambios
                    .Include(x => x.IntercambioTipoReciclables).ThenInclude(y => y.Tipo)
                    .Include(x => x.IntercambioTipoReciclables).ThenInclude(y => y.Lote).ThenInclude(z=> z.Planta)
                    .Include(x => x.Punto)
                    .Where(x => x.Id == id).FirstOrDefault();
            }
        }

        public List<Intercambio> GetBySocio(int socioId)
        {
            using (var _context = new GreenPointsContext())
            {
                return _context.Intercambios
                    .Where(x => x.SocioId == socioId).ToList();
            }
        }

        public List<IntercambioTipoReciclable> GetByPunto(int puntoId)
        {
            using (var _context = new GreenPointsContext())
            {
                return _context.IntercambiosTiposReciclables
                    .Include(x => x.Intercambio)
                    .Where(x => x.Intercambio.PuntoId == puntoId).ToList();
            }
        }
    }
}
