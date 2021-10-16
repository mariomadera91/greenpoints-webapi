using GreenPoints.Data;
using GreenPoints.Services.Interfaces;
using System.Linq;
using System.Collections.Generic;

namespace GreenPoints.Services
{
    public class LoteService : ILoteService
    {
        private ILoteRepository _loteRepository;

        public LoteService(
            ILoteRepository loteRepository)
        {
            _loteRepository = loteRepository;
        }

        public List<LoteListDto> Get(int puntoId)
        {
            return _loteRepository.GetByPunto(puntoId).Select(x => new LoteListDto()
            {
                Id = x.Id,
                Fecha = x.FechaCreacion,
                TipoMaterialNombre = x.Tipo.Nombre
            }).ToList();
        }
    }
}
