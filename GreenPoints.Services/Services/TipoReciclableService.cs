using GreenPoints.Data;
using System.Collections.Generic;
using System.Linq;

namespace GreenPoints.Services
{
    public class TipoReciclableService : ITipoReciclableService
    {
        private readonly ITipoReciclableRepository _tipoReciclableRepository;

        public TipoReciclableService(ITipoReciclableRepository tipoReciclableRepository)
        {
            _tipoReciclableRepository = tipoReciclableRepository;
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
    }
}
