using GreenPoints.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System;

namespace GreenPoints.Data
{
    public class PremioRepository : IPremioRepository
    {
        public List<Premio> Get(bool admin = false)
        {
            using (var _context = new GreenPointsContext())
            {
                return _context.Premios.Include(x => x.Sponsor)
                    .Where(x => x.Activo && (admin || (x.Stock > 0
                                && (x.VigenciaDesde <= DateTime.Now)
                                && (!x.VigenciaHasta.HasValue || x.VigenciaHasta >= DateTime.Now))))
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
            using (var _context = new GreenPointsContext())
            {
                return _context.SociosPremios.Include(x => x.Premio).Include(x => x.Codigo)
                    .Where(x => x.SocioId == socioId)
                    .ToList();
            }
        }

        public SocioPremio GetSocioPremio(int socioPremioId)
        {
            using (var _context = new GreenPointsContext())
            {
                return _context.SociosPremios.Include(x => x.Premio).Include(x => x.Codigo).Where(x => x.Id == socioPremioId).FirstOrDefault();
            }
        }

        public void CreatePremio(Premio premio)
        {
            using (var _context = new GreenPointsContext())
            {
                _context.Premios.Add(premio);
                _context.SaveChanges();
            }
        }

        public void CreatePremioCodigos(List<PremioCodigo> premioCodigos)
        {
            using (var _context = new GreenPointsContext())
            {
                foreach (var premioCodigo in premioCodigos)
                {
                    _context.PremiosCodigos.Add(premioCodigo);
                }

                _context.SaveChanges();
            }
        }

        public List<PremioCodigo> GetPremioCodigos(int premioId)
        {
            using (var _context = new GreenPointsContext())
            {
                return _context.PremiosCodigos.Where(x => x.PremioId == premioId && x.Activo).ToList();
            }
        }

        public List<PremioCodigo> GetPremioCodigosBySponsor(int sponsorId)
        {
            using (var _context = new GreenPointsContext())
            {
                return _context.PremiosCodigos.Include(x => x.Premio).Where(x => x.Premio.SponsorId == sponsorId && x.Activo).ToList();
            }
        }

        public void DisablePremioCodigos(List<PremioCodigo> premioCodigos)
        {
            using (var _context = new GreenPointsContext())
            {
                foreach (var premioCodigo in premioCodigos)
                {
                    premioCodigo.Activo = false;
                    _context.PremiosCodigos.Update(premioCodigo);
                }

                _context.SaveChanges();
            }
        }

        public void DisablePremio(List<Premio> premios)
        {
            using (var _context = new GreenPointsContext())
            {
                foreach (var premio in premios)
                {
                    premio.Activo = false;
                    _context.Premios.Update(premio);
                }

                _context.SaveChanges();
            }
        }
    }
}
