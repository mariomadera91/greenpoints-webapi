using System.Collections.Generic;

namespace GreenPoints.Domain
{
    public interface IPremioRepository
    {
        List<Premio> Get();
        Premio GetById(int id);
    }
}
