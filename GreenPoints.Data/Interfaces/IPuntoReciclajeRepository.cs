using GreenPoints.Domain;
using System.Collections.Generic;

namespace GreenPoints.Data
{
    public interface IPuntoReciclajeRepository
    {
        PuntoReciclaje GetPuntoReciclaje(int id);
        void Add(PuntoReciclaje punto);
        List<PuntoReciclaje> Get();
        public void Update(PuntoReciclaje puntoReciclaje);
        public void DeleteTipoReciclable(List<PuntoReciclajeTipoReciclable> puntoReciclajeTipoReciclables);
        public PuntoReciclaje GetByPuntoReciclajeId(int id);
    }
}
