using GreenPoints.Domain;
using System.Collections.Generic;

namespace GreenPoints.Services.Interfaces
{
    public interface IPremioService
    {
        List<Premio> Get();

        Premio GetById(int id);
    }
}
