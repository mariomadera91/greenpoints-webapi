using GreenPoints.Data;
using GreenPoints.Domain;
using System.Linq;
using System.Collections.Generic;
using System.Transactions;
using System;

namespace GreenPoints.Services
{
    public class PuntoReciclajeService : IPuntoReciclajeService
    {
        private IPuntoReciclajeRepository _puntoReciclajeRepository;
        private IUsuarioRepository _usuarioRepository;

        public PuntoReciclajeService(IPuntoReciclajeRepository puntoReciclajeRepository,
                                      IUsuarioRepository usuarioRepository)
        {
            _puntoReciclajeRepository = puntoReciclajeRepository;
            _usuarioRepository = usuarioRepository;
        }

        public void Create(CreatePuntoReciclajeDto puntoDto)
        {
            var address = AddressHelper.GetAddress(puntoDto.Latitud, puntoDto.Longitud);

            var puntoReciclaje = new PuntoReciclaje()
            {
                Nombre = puntoDto.CustomerName,
                CUIT = puntoDto.Document,
                Direccion = address,
                Latitud = puntoDto.Latitud,
                Longitud = puntoDto.Longitud,
                PuntoReciclajeTipoReciclables = new List<PuntoReciclajeTipoReciclable>(),
                Usuario = new Usuario()
                {
                    UserName = puntoDto.UserName,
                    Password = puntoDto.Password,
                    Rol = UserRol.PuntoReciclaje,
                    Activo = true
                }
            };

            puntoDto.Materials.ForEach(material =>
            {
                puntoReciclaje.PuntoReciclajeTipoReciclables.Add(new PuntoReciclajeTipoReciclable()
                {
                    TipoId = material
                });
            });

            _puntoReciclajeRepository.Add(puntoReciclaje);
        }

        public List<PuntoReciclajeListDto> Get(int? tipoId)
        {
            return _puntoReciclajeRepository.Get()
                .Where(x => !tipoId.HasValue || x.PuntoReciclajeTipoReciclables.Any(y => y.TipoId == tipoId))
                .Select(x => new PuntoReciclajeListDto()
            {
                Id = x.Id,
                Latitud = x.Latitud,
                Longitud = x.Longitud,
                Nombre = x.Nombre,
                Description = GetDescription(x.PuntoReciclajeTipoReciclables.Select(y => y.Tipo).ToList())
            }).ToList();
        }

        private string GetDescription(List<TipoReciclable> tipos)
        {
            var description = "Puedes intercambiar: ";

            if(tipos.Count == 1)
            {
                description += $"{ tipos[0].Nombre }.";
            }
            else if (tipos.Count > 1)
            {
                description += $"{ tipos[0].Nombre }";

                for (int i = 1; i < tipos.Count - 1; i++)
                {
                    description += $", { tipos[i].Nombre }";
                }

                description += $" y { tipos[tipos.Count - 1].Nombre }.";
            }

            return description;
        }

        public void Update(PuntoUpdateDto puntoUpdate)
        {
            var punto = _puntoReciclajeRepository.GetByPuntoReciclajeId(puntoUpdate.Id);

            var address = AddressHelper.GetAddress(puntoUpdate.Latitud, puntoUpdate.Longitud);

            var puntoUp = new PuntoReciclaje()
            {
                Id = puntoUpdate.Id,
                Nombre = punto.Nombre,
                CUIT = punto.CUIT,
                Direccion = address,
                Latitud = puntoUpdate.Latitud,
                Longitud = puntoUpdate.Longitud,
                UsuarioId = punto.UsuarioId,
                PuntoReciclajeTipoReciclables = punto.PuntoReciclajeTipoReciclables
            };

            using (var scope = new TransactionScope())
            {
                _puntoReciclajeRepository.DeleteTipoReciclable(punto.PuntoReciclajeTipoReciclables);
                puntoUpdate.Materials.ForEach(material =>
                {
                    punto.PuntoReciclajeTipoReciclables.Add(new PuntoReciclajeTipoReciclable()
                    {
                        TipoId = material
                    });
                });

                _puntoReciclajeRepository.Update(puntoUp);

                scope.Complete();
            }
        }

        public PuntoReciclajeGetDto GetByPuntoId(int Id)
            {
                var punto = _puntoReciclajeRepository.GetByPuntoReciclajeId(Id);
                return new PuntoReciclajeGetDto()
                {
                    Id = punto.Id,
                    Nombre = punto.Nombre,
                    CUIT = punto.CUIT,
                    Direccion = punto.Direccion,
                    Latitud = punto.Latitud,
                    Longitud = punto.Longitud,
                    UsuarioId = punto.UsuarioId,
                    Email = punto.Usuario.UserName
                };
            }

        public void Delete(int id)
        {
            var punto = _puntoReciclajeRepository.GetByPuntoReciclajeId(id);
            punto.Usuario.Activo = false;
            punto.Usuario.UserName = DateTime.Now.ToString("yyyy_MM_dd_HH_mm") + "__" + punto.Usuario.UserName;
            _usuarioRepository.Update(punto.Usuario);
        }
    }
}
