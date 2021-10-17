using GreenPoints.Domain;
using System.Collections.Generic;

namespace GreenPoints.Data
{
    public interface ILoteRepository
    {
        List<Lote> GetByPunto(int puntoId);
        List<Lote> GetActiveByPunto(int puntoId);
        Lote GetActiveByTipoRecicable(int puntoId, int tipoId);
        void Create(Lote lote);
    }
}
