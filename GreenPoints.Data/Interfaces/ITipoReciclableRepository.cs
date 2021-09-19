using GreenPoints.Domain;
using System.Collections.Generic;

namespace GreenPoints.Data
{
    public interface ITipoReciclableRepository
    {
        List<TipoReciclable> Get();
        List<TipoReciclable> GetByPunto(int puntoId);
    }
}
