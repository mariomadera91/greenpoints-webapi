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

        public IntercambioService(
            IIntercambioRepository intercambioRepository)
        {
            _intercambioRepository = intercambioRepository;
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

            var intercambioDetialDto = new IntercambioDto()
            {
                Date = intercambio.Fecha,
                Points = intercambio.Puntos,
                PuntoReciclajeName = intercambio.Punto.Nombre,
                Detail = new List<IntercambioDetailDto>()
            };

            intercambio.IntercambioTipoReciclables.ForEach(intercambioTipoReciclable =>
            {
                intercambioDetialDto.Detail.Add(new IntercambioDetailDto()
                {
                    Weight = intercambioTipoReciclable.Peso,
                    TipoReciclableName = intercambioTipoReciclable.Tipo.Nombre
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
                    LoteId = 1
                });
            }
            _intercambioRepository.Create(intercambio);
        }

    }
}
