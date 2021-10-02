using System.Collections.Generic;

namespace GreenPoints.Services
{
    public interface IPuntoReciclajeService
    {
        void Create(CreatePuntoReciclajeDto puntoDto);

        List<PuntoReciclajeListDto> Get();
    }
}
