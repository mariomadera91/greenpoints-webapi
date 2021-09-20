using GreenPoints.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
