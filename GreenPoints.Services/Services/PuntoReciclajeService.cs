using GreenPoints.Data;
using GreenPoints.Domain;
using System.Linq;
using System.Collections.Generic;

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
            var puntoReciclaje = new PuntoReciclaje()
            {
                Nombre = puntoDto.CustomerName,
                CUIT = puntoDto.Document,
                Direccion = puntoDto.Direccion,
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
    }
}
