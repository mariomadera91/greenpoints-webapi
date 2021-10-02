using GreenPoints.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace GreenPoints.Data
{
    public class PuntoReciclajeRepository : IPuntoReciclajeRepository
    {
        public void Add(PuntoReciclaje punto)
        {
            using (var _context = new GreenPointsContext())
            {
                _context.Add(punto);
                _context.SaveChanges();
            }
        }

        public PuntoReciclaje GetPuntoReciclaje(int id)
        {
            using (var _context = new GreenPointsContext())
            {
                return _context.PuntosReciclaje.Where(x => x.UsuarioId == id).FirstOrDefault();
            }
        }

        public List<PuntoReciclaje> Get()
        {
            using (var _context = new GreenPointsContext())
            {
                return _context.PuntosReciclaje
                    .Include(x => x.Usuario)
                    .Include(x => x.PuntoReciclajeTipoReciclables).ThenInclude(y => y.Tipo)
                    .Where(x => x.Usuario.Activo).ToList();
            }
        }
    }
}
