using GreenPoints.Data;
using GreenPoints.Domain;
using System;
using System.Collections.Generic;
using System.Transactions;

namespace GreenPoints.Services
{
    public class DonacionService : IDonacionService
    {
        private readonly ISocioRecicladorRepository _socioRecicladorRepository;
        private readonly IMovimientoPuntosRepository _movimientoPuntosRepository;

        public DonacionService(
            ISocioRecicladorRepository socioRecicladorRepository,
            IMovimientoPuntosRepository movimientoPuntosRepository)
        {
            _socioRecicladorRepository = socioRecicladorRepository;
            _movimientoPuntosRepository = movimientoPuntosRepository;
        }

        public void Post(CreateDonacionDto createDonacionDto)
        {
            var socioOrigen = _socioRecicladorRepository.GetById(createDonacionDto.SocioOrigenId);
            var socioDestino = _socioRecicladorRepository.GetById(createDonacionDto.SocioDestinoId);

            if(socioOrigen.Puntos < createDonacionDto.Puntos)
            {
                throw new Exception("Puntos insuficientes");
            }

            using (var scope = new TransactionScope())
            {
                socioOrigen.Puntos -= createDonacionDto.Puntos;
                socioDestino.Puntos += createDonacionDto.Puntos;

                _socioRecicladorRepository.Update(new List<SocioReciclador>()
                {
                    socioOrigen, socioDestino
                });

                _movimientoPuntosRepository.Create(new MovimientoPuntos()
                {
                    Cantidad = -createDonacionDto.Puntos,
                    Fecha = DateTime.Now,
                    SocioId = socioOrigen.Id,
                    Descripcion = $"Donación a { socioDestino.Nombre } { socioDestino.Apellido }",
                    Tipo = TipoMovimiento.Donacion
                });

                _movimientoPuntosRepository.Create(new MovimientoPuntos()
                {
                    Cantidad = createDonacionDto.Puntos,
                    Fecha = DateTime.Now,
                    SocioId = socioDestino.Id,
                    Descripcion = $"Donación de { socioOrigen.Nombre } { socioOrigen.Apellido }",
                    Tipo = TipoMovimiento.Donacion
                });

                scope.Complete();

            }
                
        }
    }
}
