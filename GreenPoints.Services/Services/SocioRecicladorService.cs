using GreenPoints.Data;
using GreenPoints.Domain;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenPoints.Services
{
    public class SocioRecicladorService : ISocioRecicladorService
    {
        private ISocioRecicladorRepository _socioRecicladorRepository;
        private IUsuarioRepository _usuarioRepository;
        public SocioRecicladorService(ISocioRecicladorRepository socioRecicladorRepository,
                                      IUsuarioRepository usuarioRepository)
        {
            _socioRecicladorRepository = socioRecicladorRepository;
            _usuarioRepository = usuarioRepository;
        }
        public void Create(CreateSocioRecicladorDto socioDto)
        {
            var usuario = new Usuario()
            {
                UserName = socioDto.Email,
                Password = socioDto.Password,
                Rol = UserRol.SocioReciclador,
                Activo = true
            };

            var usuarioDb = _usuarioRepository.AddUsuario(usuario);
            var socioReciclador = new SocioReciclador()
            {
                Apellido = socioDto.LastName,
                Nombre = socioDto.FirstName,
                FechaNac = DateTime.ParseExact(socioDto.BirthDate, "dd-MM-yyyy", null),
                Puntos = 0,
                UsuarioId = usuarioDb.Id
            };

            _socioRecicladorRepository.Add(socioReciclador);
        }

        public List<SocioRecicladorDto> Get()
        {
            var sociosRecicladores = _socioRecicladorRepository.Get();
            return sociosRecicladores.Select(x=> new SocioRecicladorDto()
            {
                SocioId = x.Id,
                Email = x.Usuario.UserName
            }).ToList();
        }
    }
}
