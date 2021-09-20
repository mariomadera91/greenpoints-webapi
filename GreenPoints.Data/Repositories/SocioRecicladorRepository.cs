﻿using GreenPoints.Domain;
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
        public SocioReciclador GetSocioReciclador(int id)
        {
            using (var _context = new GreenPointsContext())
            {
                return _context.SociosRecicladores.Where(x => x.UsuarioId == id).FirstOrDefault();
            }
        }
    }
}