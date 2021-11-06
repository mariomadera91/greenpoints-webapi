using GreenPoints.Data;
using GreenPoints.Domain;
using GreenPoints.Services.Interfaces;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Transactions;
using System;
using System.IO;

namespace GreenPoints.Services
{
    public class SponsorService: ISponsorService
    {
        private ISponsorRepository _sponsorRepository;
        private IImageService _imageService;
        private IConfiguration _configuration;
        private IPremioRepository _premioRepository;

        public SponsorService(ISponsorRepository sponsorRepository,
                              IPremioRepository premioRepository,
                              IImageService imageService,
                              IConfiguration configuration)
        {
            _sponsorRepository = sponsorRepository;
            _configuration = configuration;
            _imageService = imageService;
            _premioRepository = premioRepository;
        }

        public void AddSponsor(CreateSponsorDto sponsorDto)
        {
            byte[] bytes = (sponsorDto.Image != null) ? Convert.FromBase64String(sponsorDto.Image.base64) : null;
            var imageFileName = (sponsorDto.Image != null) ? Guid.NewGuid() + ".png" : string.Empty;
            var pathSponsor = $"{ _configuration.GetSection("imagePath").Value }\\sponsors\\{ imageFileName }";
            var pathPremio = $"{ _configuration.GetSection("imagePath").Value }\\premios\\{ imageFileName }";

            var spon = new Sponsor()
            {
                Nombre = sponsorDto.Nombre,
                Activo = true,
                Imagen = imageFileName
            };

            using (var scope = new TransactionScope())
            {
                _sponsorRepository.AddSponsor(spon);

                if (!string.IsNullOrEmpty(imageFileName))
                {
                    File.WriteAllBytes(pathSponsor, bytes);
                    File.WriteAllBytes(pathPremio, bytes);
                }

                scope.Complete();
            }
        }

        public List<SponsorDto> Get()
        {
            var sponsors = _sponsorRepository.Get();

            return sponsors.Select(x => new SponsorDto()
            {
                Id = x.Id,
                Nombre = x.Nombre,
                Activo = x.Activo,
                Imagen = $"{ _configuration.GetSection("siteUrl").Value }/sponsor/image?name={ x.Imagen }"
            }).OrderByDescending(x => x.Id).ToList();
        }

        public SponsorDto GetDetailById(int id)
        {
            var sponsor = _sponsorRepository.GetById(id);

            return new SponsorDto()
            {
                Id = sponsor.Id,
                Nombre = sponsor.Nombre,
                Activo = sponsor.Activo,
                Imagen = $"{ _configuration.GetSection("siteUrl").Value }/sponsor/image?name={ sponsor.Imagen }"
            };
        }

        public ImageUrlDto GetImage(string name)
        {
            return _imageService.GetImage(name, "Sponsors");
        }

        public void Update(SponsorDto sponsordto)
        {
            byte[] bytes = (sponsordto.ImageData != null) ? Convert.FromBase64String(sponsordto.ImageData.base64) : null;
            var imageFileName = (sponsordto.ImageData != null) ? Guid.NewGuid() + ".png" : string.Empty;
            var path = $"{ _configuration.GetSection("imagePath").Value }\\sponsors\\";

            var sponsor = _sponsorRepository.GetById(sponsordto.Id);

            using (var scope = new TransactionScope())
            {
                var spon = new Sponsor()
                {
                    Id = sponsordto.Id,
                    Nombre = sponsordto.Nombre,
                    Activo = true,
                    Imagen = !string.IsNullOrEmpty(imageFileName) ? imageFileName : sponsor.Imagen,
                };

                if (!string.IsNullOrEmpty(imageFileName))
                {
                    File.Delete(path + spon.Imagen);
                    File.WriteAllBytes(path + imageFileName, bytes);
                }

                _sponsorRepository.Update(spon);

                scope.Complete();
            }
        }
        public void Delete(int id)
        {
            var sponsor = _sponsorRepository.GetById(id);
            sponsor.Activo = false;
            var premiosCodigos = _premioRepository.GetPremioCodigosBySponsor(id);
            var premios = premiosCodigos.Select(x => x.Premio).Distinct().ToList();

            using (var scope = new TransactionScope())
            {
                _premioRepository.DisablePremioCodigos(premiosCodigos);
                _premioRepository.DisablePremio(premios);
                _sponsorRepository.Update(sponsor);
                scope.Complete();
            }
                
        }
    }
}
