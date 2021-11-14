using System;

namespace GreenPoints.WebApi.Models
{
    public class SocioRecicladorModel
    {
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public int? ReferidoId { get; set; }
    }
}
