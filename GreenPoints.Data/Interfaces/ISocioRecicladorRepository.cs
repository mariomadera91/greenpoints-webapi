using GreenPoints.Domain;
using System.Collections.Generic;

namespace GreenPoints.Data
{
    public interface ISocioRecicladorRepository
    {
        SocioReciclador GetByUsuarioId(int usuarioId);
        
        void Add(SocioReciclador socio);
        
        List<SocioReciclador> Get();

        SocioReciclador GetById(int id);

        void Update(SocioReciclador socio);

        void Update(List<SocioReciclador> socios);
    }
}
