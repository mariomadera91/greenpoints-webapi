﻿namespace GreenPoints.Domain
{
    public interface IUsuarioRepository
    {
        Usuario GetUsuario(string userName, string password);
        Usuario AddUsuario(Usuario usuario);
        Usuario GetByEmail(string email);
        void Update(Usuario usuario);
        Usuario GetById(int id);
    }
}
