using GreenPoints.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenPoints.Data
{
    public interface ISocioRecicladorRepository
    {
        SocioReciclador GetByUsuarioId(int usuarioId);
        void Add(SocioReciclador socio);
        List<SocioReciclador> Get();
        SocioReciclador GetById(int id);
    }
}
