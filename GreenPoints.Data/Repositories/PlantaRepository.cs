using GreenPoints.Domain;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

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
        public void AddPlanta(Planta planta)
        {
            using (var _context = new GreenPointsContext())
            {
                _context.Add(planta);
                _context.SaveChanges();
            }
        }
        public List<Planta> Get()
        {
            using (var _context = new GreenPointsContext())
            {
                return _context.Plantas
                    .OrderByDescending(x => x.FechaCrea)
                    .ToList();
            }
        }
        public Planta GetById(int id)
        {
            using (var _context = new GreenPointsContext())
            {
                return _context.Plantas.Where(x => x.Id == id).FirstOrDefault();
            }
        }

        public void Update(Planta plantaDto)
        {
            using (var _context = new GreenPointsContext())
            {
                _context.Update(plantaDto);
                _context.SaveChanges();
            }
        }
    }
}
