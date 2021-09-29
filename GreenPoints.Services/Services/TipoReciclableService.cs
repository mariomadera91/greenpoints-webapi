using GreenPoints.Data;
using System.Collections.Generic;
using System.Linq;

namespace GreenPoints.Services
{
    public class TipoReciclableService : ITipoReciclableService
    {
        private ITipoReciclableRepository _tipoReciclableRepository;
        private ILoteRepository _loteRepository;

        public TipoReciclableService(ITipoReciclableRepository tipoReciclableRepository,
            ILoteRepository loteRepository)
        {
            _tipoReciclableRepository = tipoReciclableRepository;
            _loteRepository = loteRepository;
        }
        public List<TipoReciclableDto> Get()
        {
            return _tipoReciclableRepository.Get().Select(x => new TipoReciclableDto()
            {
                Id = x.Id,
                Nombre = x.Nombre,
                Points = x.PuntosKg
            }).ToList();
        }

        public List<TipoReciclableDto> GetByPunto(int puntoId)
        {
            var lotes = _loteRepository.GetActiveByPunto(puntoId);
            return _tipoReciclableRepository.GetByPunto(puntoId)
                .Where(x=> lotes.Any(y => y.TipoId == x.Id ))
                .Select(x => new TipoReciclableDto()
            {
                Id = x.Id,
                Nombre = x.Nombre,
                Points = x.PuntosKg
            }).ToList();
        }
    }
}
