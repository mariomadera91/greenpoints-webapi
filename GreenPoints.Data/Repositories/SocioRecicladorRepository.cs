using GreenPoints.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenPoints.Data
{
    public class SocioRecicladorRepository : ISocioRecicladorRepository
    {
        public void Add(SocioReciclador socio)
        {
            using (var _context = new GreenPointsContext())
            {
                _context.Add(socio);
                _context.SaveChanges();
            }
        }

        public List<SocioReciclador> Get()
        {
            using (var _context = new GreenPointsContext())
            {
                return _context.SociosRecicladores.Include(x=> x.Usuario).Where(x=> x.Usuario.Activo).ToList();
            }
        }

        public SocioReciclador GetById(int id)
        {
            using (var _context = new GreenPointsContext())
            {
                return _context.SociosRecicladores.Where(x => x.Id == id).FirstOrDefault();
            }
        }

        public SocioReciclador GetByUsuarioId(int usuarioId)
        {
            using (var _context = new GreenPointsContext())
            {
                return _context.SociosRecicladores.Where(x => x.UsuarioId == usuarioId).FirstOrDefault();
            }
        }

        public void Update(SocioReciclador socio)
        {
            using (var _context = new GreenPointsContext())
            {
                _context.SociosRecicladores.Update(socio);
                _context.SaveChanges();
            }
        }

        public void Update(List<SocioReciclador> socios)
        {
            using (var _context = new GreenPointsContext())
            {
                foreach (var socio in socios)
                {
                    _context.SociosRecicladores.Update(socio);
                }
                
                _context.SaveChanges();
            }
        }
    }
}
