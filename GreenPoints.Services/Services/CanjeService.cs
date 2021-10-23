using GreenPoints.Data;
using GreenPoints.Domain;
using System;
using System.Transactions;

namespace GreenPoints.Services
{
    public class CanjeService : ICanjeService
    {
        private IPremioRepository _premioRepository;
        private ISocioRecicladorRepository _socioRecicladorRepository;
        private IMovimientoPuntosRepository _movimientoPuntosRepository;

        public CanjeService(
            IPremioRepository premioRepository,
            ISocioRecicladorRepository socioRecicladorRepository,
            IMovimientoPuntosRepository movimientoPuntosRepository)
        {
            _premioRepository = premioRepository;
            _socioRecicladorRepository = socioRecicladorRepository;
            _movimientoPuntosRepository = movimientoPuntosRepository;
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

            using (var scope = new TransactionScope())
            {
                _premioRepository.CreateSocioPremio(new SocioPremio()
                {
                    CodigoId = premioCodigo.Id,
                    Fecha = DateTime.Now,
                    PremioId = premioId,
                    SocioId = socioId
                });

                _movimientoPuntosRepository.Create(new MovimientoPuntos()
                {
                    Cantidad = -premio.Puntos,
                    Fecha = DateTime.Now,
                    SocioId = socioId,
                    Descripcion = $"Canje { premio.Nombre }",
                    Tipo = TipoMovimiento.Canje
                });

                socio.Puntos -= premio.Puntos;
                _socioRecicladorRepository.Update(socio);

                premio.Stock -= 1;
                _premioRepository.Update(premio);

                premioCodigo.Activo = false;
                _premioRepository.UpdatePremioCodigo(premioCodigo);

                scope.Complete();
            }
            

            return premioCodigo.Codigo;
        }
    }
}
