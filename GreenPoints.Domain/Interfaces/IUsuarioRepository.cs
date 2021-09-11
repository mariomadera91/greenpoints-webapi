namespace GreenPoints.Domain
{
    public interface IUsuarioRepository
    {
        Usuario GetUsuario(string userName, string password);
        Usuario AddUsuario(Usuario usuario);
    }
}
