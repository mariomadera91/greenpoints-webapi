using GreenPoints.Domain;
using System.Collections.Generic;

namespace GreenPoints.Data
{
    public interface IPlantaRepository
    {
        void AddPlanta(Planta planta);
        List<Planta> Get();
        public Planta GetById(int id);
        public void Update(Planta planta);
        List<Planta> Search();
    }
}
