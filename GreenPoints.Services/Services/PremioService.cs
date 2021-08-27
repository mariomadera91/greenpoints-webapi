using GreenPoints.Domain;
using GreenPoints.Services.Interfaces;
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

        public List<Premio> Get()
        {
            return _premioRepository.Get();
        }

        public Premio GetById(int id)
        {
            return _premioRepository.GetById(id);
        }
    }
}
