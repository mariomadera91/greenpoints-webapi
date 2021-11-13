using GreenPoints.Data;
using GreenPoints.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace GreenPoints.Services
{
    public class SocioRecicladorService : ISocioRecicladorService
    {
        private readonly ISocioRecicladorRepository _socioRecicladorRepository;
        private readonly IPremioRepository _premioRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMovimientoPuntosRepository _movimientoPuntosRepository;

        public SocioRecicladorService(
            ISocioRecicladorRepository socioRecicladorRepository,
            IPremioRepository premioRepository,
            IUsuarioRepository usuarioRepository,
            IMovimientoPuntosRepository movimientoPuntosRepository)
        {
            _socioRecicladorRepository = socioRecicladorRepository;
            _premioRepository = premioRepository;
            _usuarioRepository = usuarioRepository;
            _movimientoPuntosRepository = movimientoPuntosRepository;
        }

        public void Create(CreateSocioRecicladorDto socioDto)
        {

            var socioReciclador = new SocioReciclador()
            {
                Apellido = socioDto.LastName,
                Nombre = socioDto.FirstName,
                FechaNac = socioDto.BirthDate,
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

        public void Update(SocioUpdateDto socioUpdate)
        {
            var socio = _socioRecicladorRepository.GetById(socioUpdate.Id);
            _socioRecicladorRepository.Update(socio);
            var socioUp = new SocioReciclador()
            {
                Id = socioUpdate.Id,
                Nombre = socioUpdate.Nombre,
                Apellido = socioUpdate.Apellido,
                FechaNac = socio.FechaNac,
                Puntos = socio.Puntos,
                UsuarioId = socio.UsuarioId
            };
            _socioRecicladorRepository.Update(socioUp);
        }

        public SocioRecicladorGetDto GetBySocioId(int Id)
        {
            var socio = _socioRecicladorRepository.GetById(Id);
            return new SocioRecicladorGetDto()
            {
                SocioId = socio.Id,
                Nombre = socio.Nombre,
                Apellido = socio.Apellido,
                Email = socio.Usuario.UserName,
                fechaNac = socio.FechaNac
            };
        }

        public void Delete(int id)
        {
            var socio = _socioRecicladorRepository.GetById(id);
            socio.Usuario.Activo = false;
            socio.Usuario.UserName = DateTime.Now.ToString("yyyy_MM_dd_HH_mm") + "__" + socio.Usuario.UserName;
            _usuarioRepository.Update(socio.Usuario);
        }
        public void Referido(string referido)
        {
            using (var scope = new TransactionScope())
            {
                var usuario = _usuarioRepository.GetByEmail(referido);
                var socio = _socioRecicladorRepository.GetById(usuario.Id);
                socio.Puntos += 150;
                _socioRecicladorRepository.Update(socio);
                _movimientoPuntosRepository.Create(new MovimientoPuntos()
                {
                    Cantidad = 150,
                    Fecha = DateTime.Now,
                    SocioId = socio.Id,
                    Descripcion = $"Referido " + referido,
                    Tipo = TipoMovimiento.Referido
                });
                scope.Complete();
            }
        }

    }
}
