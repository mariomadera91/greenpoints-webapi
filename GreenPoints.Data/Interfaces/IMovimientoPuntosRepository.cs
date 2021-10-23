using GreenPoints.Domain;
using System.Collections.Generic;

namespace GreenPoints.Data
{
    public interface IMovimientoPuntosRepository
    {
        void Create(MovimientoPuntos movimientoPuntos);
        List<MovimientoPuntos> GetBySocio(int socioId);
    }
}
