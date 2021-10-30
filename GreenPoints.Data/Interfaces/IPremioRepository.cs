using GreenPoints.Domain;
using System.Collections.Generic;

namespace GreenPoints.Data
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

        List<SocioPremio> GetSocioPremioBySocio(int socioId);

        SocioPremio GetSocioPremio(int socioPremioId);

        void CreatePremio(Premio premio);

        void CreatePremioCodigos(List<PremioCodigo> premioCodigos);

        List<PremioCodigo> GetPremioCodigos(int premioId);
    }
}
