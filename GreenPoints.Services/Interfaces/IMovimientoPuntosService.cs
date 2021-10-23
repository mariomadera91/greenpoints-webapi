using System.Collections.Generic;

namespace GreenPoints.Services
{
    public interface IMovimientoPuntosService
    {
        List<MovimientoPuntosDto> GetBySocio(int socioId);
    }
}
