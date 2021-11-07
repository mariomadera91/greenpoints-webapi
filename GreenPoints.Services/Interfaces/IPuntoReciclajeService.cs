using System.Collections.Generic;

namespace GreenPoints.Services
{
    public interface IPuntoReciclajeService
    {
        void Create(CreatePuntoReciclajeDto puntoDto);

        List<PuntoReciclajeListDto> Get(int? tipoId);
        public void Update(PuntoUpdateDto puntoUpdate);

        public PuntoReciclajeGetDto GetByPuntoId(int Id);
    }
}
