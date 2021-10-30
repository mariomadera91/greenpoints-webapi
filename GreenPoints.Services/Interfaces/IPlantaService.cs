using GreenPoints.Domain;
using System.Collections.Generic;

namespace GreenPoints.Services
{
    public interface IPlantaService
    {
        List<PlantaSearchDto> GetSearch();
        void AddPlanta(CreatePlantaDto createPlantaDto);
        List<PlantaDto> Get();
        PlantaDto GetDetailById(int id);

        void Update(PlantaDto plantaDto);
        void Delete(int id);
    }
}
