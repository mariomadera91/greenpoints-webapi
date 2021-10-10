using GreenPoints.Data;
using GreenPoints.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

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

            var socioReciclador = new SocioReciclador()
            {
                Apellido = socioDto.LastName,
                Nombre = socioDto.FirstName,
                FechaNac = DateTime.ParseExact(socioDto.BirthDate, "dd-MM-yyyy", null),
                Puntos = 0,
                Usuario = new Usuario()
                {
                    UserName = socioDto.Email,
                    Password = socioDto.Password,
                    Rol = UserRol.SocioReciclador,
                    Activo = true
                }
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

        public int GetPuntos(int socioId)
        {
            var socioReciclador = _socioRecicladorRepository.GetById(socioId);

            return socioReciclador.Puntos;
        }
    }
}
