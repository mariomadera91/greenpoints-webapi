using GreenPoints.Domain;
using System.Collections.Generic;

namespace GreenPoints.Services.Interfaces
{
    public interface ILoteService
    {
        List<LoteListDto> Get(int puntoId);

        Lote Post(int puntoId, int tipoReciclableId);

        void Update(int loteId, int plantaId);

        LoteDto GetbyId(int loteId);
    }
}
