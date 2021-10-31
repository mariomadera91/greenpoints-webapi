using System.Collections.Generic;

namespace GreenPoints.Services
{
    public interface ITipoReciclableService
    {
        List<TipoReciclableDto> Get();

        List<TipoReciclableDto> GetByPunto(int puntoId, bool onlyOpenedLote);

        ImageUrlDto GetImage(string name);

        void AddTipoReciclable(CreateTipoReciclableDto createTipoReciclableDto);
    }
}
