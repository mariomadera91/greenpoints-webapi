using GreenPoints.Data;
using System.Linq;
using System.Collections.Generic;

namespace GreenPoints.Services
{
    public class MovimientoPuntosService : IMovimientoPuntosService
    {
        private IMovimientoPuntosRepository _movimientoPuntosRepository;

        public MovimientoPuntosService(IMovimientoPuntosRepository movimientoPuntosRepository)
        {
            _movimientoPuntosRepository = movimientoPuntosRepository;
        }

        public List<MovimientoPuntosDto> GetBySocio(int socioId)
        {
            return _movimientoPuntosRepository.GetBySocio(socioId).Select(x => new MovimientoPuntosDto()
            {
                Tipo = x.Tipo.ToString(),
                Cantidad = x.Cantidad > 0 ? $"+{ x.Cantidad.ToString() }" : $"{ x.Cantidad.ToString() }",
                Fecha = x.Fecha,
                Descripcion = x.Descripcion,
                Aumentar = x.Cantidad > 0 ? true : false
            }).ToList();
        }
    }
}
