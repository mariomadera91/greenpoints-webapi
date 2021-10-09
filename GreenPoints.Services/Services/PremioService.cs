using GreenPoints.Domain;
using System.Linq;
using System.Collections.Generic;

namespace GreenPoints.Services.Services
{
    public class PremioService : IPremioService
    {
        public IPremioRepository _premioRepository { get; set; }

        public PremioService(IPremioRepository premioRepository)
        {
            _premioRepository = premioRepository;
        }

        public List<PremioListDto> Get()
        {
            var premios = _premioRepository.Get();

            return premios.Select(x => new PremioListDto()
            {
                Id = x.Id,
                Description = x.Descripcion,
                Points = x.Puntos.ToString(),
                SponsorName = x.Sponsor.Nombre
            }).ToList();
        }

        public Premio GetById(int id)
        {
            return _premioRepository.GetById(id);
        }
    }
}
