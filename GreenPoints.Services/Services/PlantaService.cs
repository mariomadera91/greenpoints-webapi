using GreenPoints.Data;
using System.Linq;
using System.Collections.Generic;

namespace GreenPoints.Services
{
    public class PlantaService : IPlantaService
    {
        private readonly IPlantaRepository _plantaRepository;
        public PlantaService(IPlantaRepository plantaRepository)
        {
            _plantaRepository = plantaRepository;
        }
        public List<PlantaSearchDto> GetSearch()
        {
            return _plantaRepository.Search().Select(x => new PlantaSearchDto()
            {
                Id = x.Id,
                Nombre = x.Nombre,
                Direccion = x.Direccion
            }).ToList();
        }
    }
}
