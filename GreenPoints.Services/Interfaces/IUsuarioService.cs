using System.Threading.Tasks;

namespace GreenPoints.Services.Interfaces
{
    public interface IUsuarioService
    {
        UsuarioDto Get(string userName, string password);

        Task Reset(string email);
    }
}
