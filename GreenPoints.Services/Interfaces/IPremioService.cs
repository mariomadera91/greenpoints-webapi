using GreenPoints.Domain;
using System.Collections.Generic;

namespace GreenPoints.Services
{
    public interface IPremioService
    {
        List<PremioListDto> Get();

        Premio GetById(int id);
    }
}
