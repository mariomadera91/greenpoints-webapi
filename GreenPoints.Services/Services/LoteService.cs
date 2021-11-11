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
        private readonly ILoteRepository _loteRepository;
        private readonly IConfiguration _configuration;
        private readonly IIntercambioRepository _intercambioRepository;

        public LoteService(
            ILoteRepository loteRepository,
            IConfiguration configuration,
            IIntercambioRepository intercambioRepository)
        {
            _loteRepository = loteRepository;
            _configuration = configuration;
            _intercambioRepository = intercambioRepository;
        }

        public List<LoteListDto> Get(int puntoId)
        {
            var intercambiosTiposReciclables = _intercambioRepository.GetByPunto(puntoId);
            var lotes = _loteRepository.GetByPunto(puntoId);
            var lotesListDto = new List<LoteListDto>();

            foreach (var lote in lotes)
            {
                lotesListDto.Add(new LoteListDto()
                {
                    Id = lote.Id,
                    Fecha = lote.Abierto ? lote.FechaCreacion : lote.FechaCierre,
                    TipoMaterialNombre = lote.Tipo.Nombre,
                    Imagen = $"{ _configuration.GetSection("siteUrl").Value }/tipo-reciclable/image?name={ lote.Tipo.Imagen }",
                    Abierto = lote.Abierto,
                    Kilos = (decimal)intercambiosTiposReciclables.Where(x => x.LoteId == lote.Id).Sum(x => x.Peso)
                });
            }

            return lotesListDto;
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
