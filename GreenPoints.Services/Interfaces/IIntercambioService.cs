using GreenPoints.Domain;
using System.Collections.Generic;

namespace GreenPoints.Services
{
    public interface IIntercambioService
    {
        List<IntercambioListDto> GetBySocio(int socioId);
        IntercambioDto GetById(int id);

        void Post(CreateIntercambioDto intercambioDto);
    }
}
