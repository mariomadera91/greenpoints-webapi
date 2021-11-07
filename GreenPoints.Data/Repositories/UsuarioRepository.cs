using GreenPoints.Domain;
using System.Linq;

namespace GreenPoints.Data
{
    public class UsuarioRepository : IUsuarioRepository
    {
        public Usuario AddUsuario(Usuario usuario)
        {
            using (var _context = new GreenPointsContext())
            {
                _context.Add(usuario);
                _context.SaveChanges();
                return  _context.Usuarios.Where(x => x.UserName == usuario.UserName && x.Activo).First();
            }
        }

        public Usuario GetUsuario(string userName, string password)
        {
            using (var _context = new GreenPointsContext())
            {
                return _context.Usuarios.Where(x => x.UserName == userName && x.Password == password).FirstOrDefault();
            }
        }

        public Usuario GetByEmail(string email)
        {
            using (var _context = new GreenPointsContext())
            {
                return _context.Usuarios.Where(x => x.UserName == email && x.Activo).FirstOrDefault();
            }
        }

        public Usuario GetById(int id)
        {
            using (var _context = new GreenPointsContext())
            {
                return _context.Usuarios.Where(x => x.Id == id && x.Activo).FirstOrDefault();
            }
        }

        public void Update(Usuario usuario)
        {
            using (var _context = new GreenPointsContext())
            {
                _context.Usuarios.Update(usuario);
                _context.SaveChanges();
            }
        }
    }
}
