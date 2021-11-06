using System.Collections.Generic;

namespace GreenPoints.Services
{
    public interface ITipoReciclableService
    {
        List<TipoReciclableDto> Get();

        TipoReciclableDto GetById(int id);

        List<TipoReciclableDto> GetByPunto(int puntoId, bool onlyOpenedLote);

        ImageUrlDto GetImage(string name);

        TipoReciclableDto GetDetailById(int id);

        void AddTipoReciclable(CreateTipoReciclableDto createTipoReciclableDto);

        void Update(TipoReciclableDto tipoReciclableDto);

        void Delete(int id);
    }
}
