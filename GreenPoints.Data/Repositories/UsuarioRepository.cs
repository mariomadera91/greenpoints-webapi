using GreenPoints.Domain;
using System.Linq;

namespace GreenPoints.Data
{
    public class UsuarioRepository : IUsuarioRepository
    {
        public Usuario GetUsuario(string userName, string password)
        {
            using (var _context = new GreenPointsContext())
            {
                return _context.Usuarios.Where(x => x.UserName == userName && x.Password == password).FirstOrDefault();
            }
        }
    }
}
