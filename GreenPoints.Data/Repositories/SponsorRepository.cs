using GreenPoints.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenPoints.Data
{
    public class SponsorRepository :ISponsorRepository
    {
        public void AddSponsor(Sponsor sponsor)
        {
            using (var _context = new GreenPointsContext())
            {
                _context.Add(sponsor);
                _context.SaveChanges();
            }
        }
        public List<Sponsor> Get()
        {
            using (var _context = new GreenPointsContext())
            {
                return _context.Sponsors
                    .OrderByDescending(x => x.Nombre)
                    .ToList();
            }
        }
        public Sponsor GetById(int id)
        {
            using (var _context = new GreenPointsContext())
            {
                return _context.Sponsors.Where(x => x.Id == id).FirstOrDefault();
            }
        }

        public void Update(Sponsor sponsorDto)
        {
            using (var _context = new GreenPointsContext())
            {
                _context.Update(sponsorDto);
                _context.SaveChanges();
            }
        }
    }
}
