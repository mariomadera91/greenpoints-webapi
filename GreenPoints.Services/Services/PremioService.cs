using GreenPoints.Domain;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System;
using GreenPoints.Data;

namespace GreenPoints.Services
{
    public class PremioService : IPremioService
    {
        private IPremioRepository _premioRepository;
        private ISocioRecicladorRepository _socioRecicladorRepository;
        private IImageService _imageService;
        private IConfiguration _configuration;

        public PremioService(
            IPremioRepository premioRepository,
            ISocioRecicladorRepository socioRecicladorRepository,
            IImageService imageService,
            IConfiguration configuration)
        {
            _premioRepository = premioRepository;
            _socioRecicladorRepository = socioRecicladorRepository;
            _imageService = imageService;
            _configuration = configuration;
        }

        public List<PremioListDto> Get()
        {
            var premios = _premioRepository.Get();

            return premios.Select(x => new PremioListDto()
            {
                Id = x.Id,
                Nombre = x.Nombre,
                Points = x.Puntos.ToString(),
                SponsorName = x.Sponsor.Nombre,
                Imagen = $"{ _configuration.GetSection("siteUrl").Value }/premio/image?name={ x.Imagen }"
            }).ToList();
        }

        public PremioDto GetDetailById(int id)
        {
            var premio = _premioRepository.GetById(id);

            return new PremioDto()
            {
                Id = premio.Id,
                Name = premio.Nombre,
                Description = premio.Descripcion,
                Desde = premio.VigenciaDesde,
                Hasta = premio.VigenciaHasta,
                Puntos = premio.Puntos,
                Observacion = premio.Observacion,
                Imagen = $"{ _configuration.GetSection("siteUrl").Value }/premio/image?name={ premio.Imagen }"
            };
        }

        public ImageDto GetImage(string name)
        {
            return _imageService.GetImage(name, "Premios");
        }

        public string Exchange(int premioId, int socioId)
        {
            var socio = _socioRecicladorRepository.GetById(socioId);
            var premio = _premioRepository.GetById(premioId);
            var premioCodigo = _premioRepository.GetPremioCodigo(premioId);

            if((socio.Puntos - premio.Puntos) < 0)
            {
                throw new Exception("No tiene puntos suficientes");
            }

            _premioRepository.CreateSocioPremio(new SocioPremio()
            {
                CodigoId = premioCodigo.Id,
                Fecha = DateTime.Now,
                PremioId = premioId,
                SocioId = socioId
            });

            socio.Puntos -= premio.Puntos;
            _socioRecicladorRepository.Update(socio);

            premio.Stock -= 1;
            _premioRepository.Update(premio);

            premioCodigo.Activo = false;
            _premioRepository.UpdatePremioCodigo(premioCodigo);

            return premioCodigo.Codigo;
        }

        public List<PremioListDto> GetTop()
        {
            var topPremios = _premioRepository.GetTop();

            if(topPremios.Count < 5)
            {
                var premios = _premioRepository.Get();
                foreach (var premio in premios)
                {
                    if(!topPremios.Contains(premio))
                    {
                        topPremios.Add(premio);
                    }
                    if (topPremios.Count == 5)
                        break;
                }
            }

            return topPremios.Select(x => new PremioListDto()
            {
                Id = x.Id,
                Nombre = x.Nombre,
                Points = x.Puntos.ToString(),
                SponsorName = x.Sponsor.Nombre,
                Imagen = $"{ _configuration.GetSection("siteUrl").Value }/premio/image?name={ x.Imagen }"
            }).ToList();
        }

        public List<SocioPremioListDto> GetSocioPremioBySocio(int socioId)
        {
            var sociosPremios = _premioRepository.GetSocioPremioBySocio(socioId);

            return sociosPremios.Select(x => new SocioPremioListDto()
            {
                Id = x.Id,
                Nombre = x.Premio.Nombre,
                Hasta = x.Premio.VigenciaHasta,
                Obtencion = x.Fecha,
                Imagen = $"{ _configuration.GetSection("siteUrl").Value }/premio/image?name={ x.Premio.Imagen }"
            }).OrderByDescending(x => x.Obtencion).ToList();
        }

        public SocioPremioDto GetSocioPremio(int socioPremioId)
        {
            var socioPremio = _premioRepository.GetSocioPremio(socioPremioId);

            return new SocioPremioDto()
            {
                Id = socioPremio.Id,
                Nombre = socioPremio.Premio.Nombre,
                Descripcion = socioPremio.Premio.Descripcion,
                Observacion = socioPremio.Premio.Observacion,
                Hasta = socioPremio.Premio.VigenciaHasta,
                Codigo = socioPremio.Codigo.Codigo,
                Imagen = $"{ _configuration.GetSection("siteUrl").Value }/premio/image?name={ socioPremio.Premio.Imagen }"
            };
        }
    }
}
