using GreenPoints.Data;
using GreenPoints.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GreenPoints.Services
{
    public class IntercambioService : IIntercambioService
    {
        private IIntercambioRepository _intercambioRepository;
        private ILoteRepository _loteRepository;

        public IntercambioService(
            IIntercambioRepository intercambioRepository,
            ILoteRepository loteRepository)
        {
            _intercambioRepository = intercambioRepository;
            _loteRepository = loteRepository;
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
                                    intercambioTipoReciclable.Lote.Planta.Direccion : intercambio.Punto.Direccion
                });
            });

            return intercambioDetialDto;
        }

        public void Post(CreateIntercambioDto createIntercambioDto)
        {

            var intercambio = new Intercambio()
            {
                PuntoId = createIntercambioDto.PuntoId,
                SocioId = createIntercambioDto.SocioId,
                Fecha = DateTime.Today,
                Puntos = createIntercambioDto.TipoReciclaje.Sum(x=>x.Puntos),
                IntercambioTipoReciclables = new List<IntercambioTipoReciclable>()

            };

            foreach (var tipoReciclaje in createIntercambioDto.TipoReciclaje)
            {
                intercambio.IntercambioTipoReciclables.Add(new IntercambioTipoReciclable()
                {
                    TipoId = tipoReciclaje.TipoId,
                    Peso = tipoReciclaje.Peso,
                    Puntos = tipoReciclaje.Puntos,
                    LoteId = _loteRepository.GetActiveByTipoRecicable(createIntercambioDto.PuntoId,  tipoReciclaje.TipoId).Id
                });
            }
            _intercambioRepository.Create(intercambio);
        }

    }
}
