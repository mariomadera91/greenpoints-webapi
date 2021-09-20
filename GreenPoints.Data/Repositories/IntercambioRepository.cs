﻿using GreenPoints.Domain;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GreenPoints.Data
{
    public class IntercambioRepository : IIntercambioRepository
    {
        public Intercambio GetById(int id)
        {
            using (var _context = new GreenPointsContext())
            {
                return _context.Intercambios
                    .Include(x => x.IntercambioTipoReciclables).ThenInclude(y => y.Tipo)
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
    }
}