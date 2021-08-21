using GreenPoints.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GreenPoints.Services.Interfaces
{
    public interface IPremioService
    {
        Task<List<Premio>> Get();

        Task<Premio> GetById(int id);
    }
}
