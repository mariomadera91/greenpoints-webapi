using System.Collections.Generic;

namespace GreenPoints.Services
{
    public interface ISocioRecicladorService
    {
        void Create(CreateSocioRecicladorDto socioDto);
        List<SocioRecicladorDto> Get();
        int GetPuntos(int socioId);
    }
}
