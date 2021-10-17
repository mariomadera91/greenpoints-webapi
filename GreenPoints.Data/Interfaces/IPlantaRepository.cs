using GreenPoints.Domain;
using System.Collections.Generic;

namespace GreenPoints.Data
{
    public interface IPlantaRepository
    {
        List<Planta> Search();
    }
}
