using GreenPoints.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System;

namespace GreenPoints.Data
{
    public class PremioRepository : IPremioRepository
    {
        public List<Premio> Get()
        {
            using (var _context = new GreenPointsContext())
            {
                return _context.Premios.Include(x => x.Sponsor)
                    .Where(x => x.Activo && x.Stock > 0
                                && (x.VigenciaDesde <= DateTime.Now)
                                && (!x.VigenciaHasta.HasValue || x.VigenciaHasta >= DateTime.Now))
                    .OrderByDescending(x => x.Fecha)
                    .ToList();
            }
        }

        public Premio GetById(int id)
        {
            using (var _context = new GreenPointsContext())
            {
                return _context.Premios.Where(x => x.Id == id).FirstOrDefault();
            }
        }

        public void Update(Premio premio)
        {
            using (var _context = new GreenPointsContext())
            {
                _context.Premios.Update(premio);
                _context.SaveChanges();
            }
        }

        public PremioCodigo GetPremioCodigo(int premioId)
        {
            using (var _context = new GreenPointsContext())
            {
                return _context.PremiosCodigos.Where(x => x.PremioId == premioId && x.Activo).First();
            }
        }

        public void UpdatePremioCodigo(PremioCodigo premioCodigo)
        {
            using (var _context = new GreenPointsContext())
            {
                _context.PremiosCodigos.Update(premioCodigo);
                _context.SaveChanges();
            }
        }


        public void CreateSocioPremio(SocioPremio socioPremio)
        {
            using (var _context = new GreenPointsContext())
            {
                _context.SociosPremios.Add(socioPremio);
                _context.SaveChanges();
            }
        }

        public List<Premio> GetTop()
        {
            using (var _context = new GreenPointsContext())
            {
                var premios = _context.SociosPremios
                    .Include(x => x.Premio).ThenInclude(y => y.Sponsor)
                    .Where(x => x.Premio.Activo)
                    .ToList()
                    .GroupBy(x => x.PremioId).Select(m => new
                    {
                        PremioId = m.Key,
                        Premio = m.First().Premio,
                        Count = m.Count()
                    })
                    .OrderByDescending(x => x.Count)
                    .Take(5)
                    .Select(x => x.Premio)
                    .ToList();

                return premios;
            }
        }

        public List<SocioPremio> GetSocioPremioBySocio(int socioId)
        {
            throw new NotImplementedException();
        }

        public SocioPremio GetSocioPremio(int socioPremioId)
        {
            throw new NotImplementedException();
        }
    }
}
