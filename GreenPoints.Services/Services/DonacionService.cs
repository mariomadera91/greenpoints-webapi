using GreenPoints.Data;
using GreenPoints.Domain;
using System;
using System.Collections.Generic;

namespace GreenPoints.Services
{
    public class DonacionService : IDonacionService
    {
        private readonly ISocioRecicladorRepository _socioRecicladorRepository;
        public DonacionService(ISocioRecicladorRepository socioRecicladorRepository)
        {
            _socioRecicladorRepository = socioRecicladorRepository;
        }

        public void Post(CreateDonacionDto createDonacionDto)
        {
            var socioOrigen = _socioRecicladorRepository.GetById(createDonacionDto.SocioOrigenId);
            var socioDestino = _socioRecicladorRepository.GetById(createDonacionDto.SocioDestinoId);

            if(socioOrigen.Puntos < createDonacionDto.Puntos)
            {
                throw new Exception("Puntos insuficientes");
            }

            socioOrigen.Puntos -= createDonacionDto.Puntos;
            socioDestino.Puntos += createDonacionDto.Puntos;

            _socioRecicladorRepository.Update(new List<SocioReciclador>()
            {
                socioOrigen, socioDestino
            });
        }
    }
}
