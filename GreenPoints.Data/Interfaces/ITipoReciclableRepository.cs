using GreenPoints.Domain;
using System.Collections.Generic;

namespace GreenPoints.Data
{
    public interface ITipoReciclableRepository
    {
        List<TipoReciclable> Get();
        List<TipoReciclable> GetByPunto(int puntoId);
        public void AddTipoReciclable(TipoReciclable tipoReciclable);
        public void Update(TipoReciclable tipoReciclableDto);
        public TipoReciclable GetById(int id);

    }
}
