using GreenPoints.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenPoints.Data
{
    public interface ILoteRepository
    {
        List<Lote> GetActiveByPunto(int puntoId);
        Lote GetActiveByTipoRecicable(int puntoId, int tipoId);

    }
}
