using GreenPoints.Data;
using GreenPoints.Services.Interfaces;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using GreenPoints.Domain;
using System;

namespace GreenPoints.Services
{
    public class LoteService : ILoteService
    {
        private ILoteRepository _loteRepository;
        private IConfiguration _configuration;

        public LoteService(
            ILoteRepository loteRepository,
            IConfiguration configuration)
        {
            _loteRepository = loteRepository;
            _configuration = configuration;
        }

        public List<LoteListDto> Get(int puntoId)
        {
            return _loteRepository.GetByPunto(puntoId).Select(x => new LoteListDto()
            {
                Id = x.Id,
                Fecha = x.Abierto ? x.FechaCreacion : x.FechaCierre,
                TipoMaterialNombre = x.Tipo.Nombre,
                Imagen = $"{ _configuration.GetSection("siteUrl").Value }/tipo-reciclable/image?name={ x.Tipo.Imagen }",
                Abierto = x.Abierto
            }).ToList();
        }

        public LoteDto GetbyId(int loteId)
        {
            var lote = _loteRepository.GetById(loteId);

            return new LoteDto()
            {
                Id = lote.Id,
                Abierto = lote.Abierto,
                FechaCreacion = lote.FechaCreacion,
                FechaCierre = lote.FechaCierre,
                TipoMaterialNombre = lote.Tipo.Nombre,
                PlantaNombre = (lote.Planta != null) ? lote.Planta.Nombre : null,
                Imagen = $"{ _configuration.GetSection("siteUrl").Value }/tipo-reciclable/image?name={ lote.Tipo.Imagen }"
            };
        }

        public Lote Post(int puntoId, int tipoReciclableId)
        {
            var loteDb = _loteRepository.GetActiveByTipoRecicable(puntoId, tipoReciclableId);

            if(loteDb != null)
            {
                return null;
            }

            var lote = new Lote()
            {
                PuntoId = puntoId,
                TipoId = tipoReciclableId,
                Abierto = true,
                FechaCreacion = DateTime.Now
            };

            _loteRepository.Create(lote);

            return lote;
        }

        public void Update(int loteId, int plantaId)
        {
            var lote = _loteRepository.GetById(loteId);

            lote.PlantaId = plantaId;
            lote.FechaCierre = DateTime.Now;
            lote.Abierto = false;

            _loteRepository.Update(lote);
        }
    }
}
