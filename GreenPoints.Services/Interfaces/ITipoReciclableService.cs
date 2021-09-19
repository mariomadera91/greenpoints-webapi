using GreenPoints.Domain;
using System.Collections.Generic;

namespace GreenPoints.Services
{
    public interface ITipoReciclableService
    {
        List<TipoReciclableDto> Get();
    }
}
