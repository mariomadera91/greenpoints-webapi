using System.Collections.Generic;

namespace GreenPoints.Domain
{
    public interface IPremioRepository
    {
        List<Premio> Get();
        Premio GetById(int id);
        void Update(Premio premio);

        PremioCodigo GetPremioCodigo(int premioId);
        void UpdatePremioCodigo(PremioCodigo premioCodigo);

        void CreateSocioPremio(SocioPremio socioPremio);

        List<Premio> GetTop();
    }
}
