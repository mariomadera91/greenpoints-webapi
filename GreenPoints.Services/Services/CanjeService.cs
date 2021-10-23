using GreenPoints.Data;
using GreenPoints.Domain;
using System;

namespace GreenPoints.Services
{
    public class CanjeService : ICanjeService
    {
        private IPremioRepository _premioRepository;
        private ISocioRecicladorRepository _socioRecicladorRepository;

        public CanjeService(
            IPremioRepository premioRepository,
            ISocioRecicladorRepository socioRecicladorRepository)
        {
            _premioRepository = premioRepository;
            _socioRecicladorRepository = socioRecicladorRepository;
        }

        public string Post(int premioId, int socioId)
        {
            var socio = _socioRecicladorRepository.GetById(socioId);
            var premio = _premioRepository.GetById(premioId);
            var premioCodigo = _premioRepository.GetPremioCodigo(premioId);

            if ((socio.Puntos - premio.Puntos) < 0)
            {
                throw new Exception("No tiene puntos suficientes");
            }

            _premioRepository.CreateSocioPremio(new SocioPremio()
            {
                CodigoId = premioCodigo.Id,
                Fecha = DateTime.Now,
                PremioId = premioId,
                SocioId = socioId
            });

            socio.Puntos -= premio.Puntos;
            _socioRecicladorRepository.Update(socio);

            premio.Stock -= 1;
            _premioRepository.Update(premio);

            premioCodigo.Activo = false;
            _premioRepository.UpdatePremioCodigo(premioCodigo);

            return premioCodigo.Codigo;
        }
    }
}
