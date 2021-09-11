using System;

namespace GreenPoints.Domain
{
    public class SocioReciclador: IdentifierEntity
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaNac { get; set; }

        public int Puntos { get; set; }
 
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }


    }
}
