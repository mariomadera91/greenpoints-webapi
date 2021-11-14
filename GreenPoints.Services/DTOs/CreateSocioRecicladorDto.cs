using System;

namespace GreenPoints.Services
{
    public class CreateSocioRecicladorDto
    {
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public int? ReferidoId { get; set; }
    }
}
