using GreenPoints.Data;
using GreenPoints.Domain;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
