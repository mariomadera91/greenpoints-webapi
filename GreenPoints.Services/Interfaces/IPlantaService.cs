using System.Collections.Generic;

namespace GreenPoints.Services
{
    public interface IPlantaService
    {
        List<PlantaSearchDto> GetSearch();
    }
}
