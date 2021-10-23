using GreenPoints.Data;
using GreenPoints.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace GreenPoints.Services
{
    public class IntercambioService : IIntercambioService
    {
        private IIntercambioRepository _intercambioRepository;
        private ILoteRepository _loteRepository;
        private IMovimientoPuntosRepository _movimientoPuntosRepository;

        public IntercambioService(
            IIntercambioRepository intercambioRepository,
            ILoteRepository loteRepository,
            IMovimientoPuntosRepository movimientoPuntosRepository)
        {
            _intercambioRepository = intercambioRepository;
            _loteRepository = loteRepository;
            _movimientoPuntosRepository = movimientoPuntosRepository;
        }

        public List<IntercambioListDto> GetBySocio(int socioId)
        {
            var intercambios = _intercambioRepository.GetBySocio(socioId);

            var intercambiosDto = new List<IntercambioListDto>();

            int intercambioNumber = 1;
            
            foreach (var intercambio in intercambios)
            {
                intercambiosDto.Add(new IntercambioListDto()
                {
                    Id = intercambio.Id,
                    Date = intercambio.Fecha,
                    Name = $"Mi intercambio #{ intercambioNumber }",
                    Points = intercambio.Puntos
                });

                intercambioNumber++;
            }

            return intercambiosDto;
        }

        public IntercambioDto GetById(int id)
        {
            var intercambio = _intercambioRepository.GetById(id);

            var intercambioNumber = _intercambioRepository.GetBySocio(intercambio.SocioId).Where(x => x.Id < intercambio.Id).Count() + 1;

            var intercambioDetialDto = new IntercambioDto()
            {
                Id = intercambio.Id,
                Name = $"Mi intercambio #{ intercambioNumber }",
                Date = intercambio.Fecha,
                Points = intercambio.Puntos,
                PuntoReciclajeName = intercambio.Punto.Nombre,
                PuntoReciclajeAddress = intercambio.Punto.Direccion,
                Detail = new List<IntercambioDetailDto>()
            };

            intercambio.IntercambioTipoReciclables.ForEach(intercambioTipoReciclable =>
            {
                intercambioDetialDto.Detail.Add(new IntercambioDetailDto()
                {
                    Weight = intercambioTipoReciclable.Peso,
                    TipoReciclableName = intercambioTipoReciclable.Tipo.Nombre,
                    PlantaName = (intercambioTipoReciclable.Lote.Planta != null) ? 
                                    intercambioTipoReciclable.Lote.Planta.Nombre : intercambio.Punto.Nombre,
                    PlantaAddress = (intercambioTipoReciclable.Lote.Planta != null) ? 
                                    intercambioTipoReciclable.Lote.Planta.Direccion : intercambio.Punto.Direccion,
                    ClosedLote = !intercambioTipoReciclable.Lote.Abierto
                });
            });

            return intercambioDetialDto;
        }

        public void Post(CreateIntercambioDto createIntercambioDto)
        {
            using (var scope = new TransactionScope())
            {
                var intercambio = new Intercambio()
                {
                    PuntoId = createIntercambioDto.PuntoId,
                    SocioId = createIntercambioDto.SocioId,
                    Fecha = DateTime.Today,
                    Puntos = createIntercambioDto.TipoReciclaje.Sum(x => x.Puntos),
                    IntercambioTipoReciclables = new List<IntercambioTipoReciclable>()
                };

                var intercambioNumber = _intercambioRepository.GetBySocio(intercambio.SocioId).Count() + 1;

                foreach (var tipoReciclaje in createIntercambioDto.TipoReciclaje)
                {
                    intercambio.IntercambioTipoReciclables.Add(new IntercambioTipoReciclable()
                    {
                        TipoId = tipoReciclaje.TipoId,
                        Peso = tipoReciclaje.Peso,
                        Puntos = tipoReciclaje.Puntos,
                        LoteId = _loteRepository.GetActiveByTipoRecicable(createIntercambioDto.PuntoId, tipoReciclaje.TipoId).Id
                    });
                }
                
                _intercambioRepository.Create(intercambio);

                _movimientoPuntosRepository.Create(new MovimientoPuntos()
                {
                    Cantidad = intercambio.Puntos,
                    Fecha = DateTime.Now,
                    SocioId = intercambio.SocioId,
                    Descripcion = $"Intercambio #{ intercambioNumber.ToString() }",
                    Tipo = TipoMovimiento.Intercambio
                });

                scope.Complete();
            }
        }

    }
}
