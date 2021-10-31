using System;

namespace GreenPoints.Domain
{
    public class Usuario: ImageEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public UserRol Rol { get; set; }
        public DateTime? LastPasswordReset { get; set; }
    }
}
