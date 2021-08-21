using GreenPoints.Data.Context;
using GreenPoints.Domain.Entities;
using GreenPoints.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace GreenPoints.Services.Services
{
    public class PremioService : IPremioService
    {
        public async Task<List<Premio>> Get()
        {
            var premios = new List<Premio>();

            premios.Add(new Premio()
            {
                Id = 1,
                Descripcion = "50% Starbucks",
                Puntos = 30,
                Activo = true,
                Fecha = new DateTime(2021, 8, 20),
                Stock = 10
            });

            premios.Add(new Premio()
            {
                Id = 2,
                Descripcion = "2 x 1 Big Mac",
                Puntos = 30,
                Activo = true,
                Fecha = new DateTime(2021, 8, 20),
                Stock = 10
            });

            return premios;
        }

        public async Task<Premio> GetById(int id)
        {
            var context = new GreenPointsContext();

            return new Premio()
            {
                Id = 1,
                Descripcion = "50% Starbucks",
                Puntos = 30,
                Activo = true,
                Fecha = new DateTime(2021, 8, 20),
                Stock = 10
            };
        }
    }
}
