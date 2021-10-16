using GreenPoints.Domain;

namespace GreenPoints.Services
{
    public class UsuarioDto
    {
        public string User { get; set; }
        public string Nombre { get; set; }
        public string Imagen { get; set; }
        public UserRol Rol { get; set; }
        public int? Id { get; set; }
        public string Token { get; set; }
    }
}
