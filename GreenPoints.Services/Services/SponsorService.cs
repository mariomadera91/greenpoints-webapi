using GreenPoints.Data;
using GreenPoints.Domain;
using System;
using GreenPoints.Services.Interfaces;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;


namespace GreenPoints.Services
{
    public class SponsorService: ISponsorService
    {
        private ISponsorRepository _sponsorRepository;
        private IImageService _imageService;
        private IConfiguration _configuration;
        public SponsorService(ISponsorRepository sponsorRepository,
                              IImageService imageService,
                              IConfiguration configuration)
        {
            _sponsorRepository = sponsorRepository;
            _configuration = configuration;
            _imageService = imageService;
        }
        public void AddSponsor(CreateSponsorDto sponsorDto)
        {
            var spon = new Sponsor()
            {
                Nombre = sponsorDto.Nombre,
                Activo = true
            };
            _sponsorRepository.AddSponsor(spon);
        }

        public List<SponsorDto> Get()
        {
            var sponsors = _sponsorRepository.Get();

            return sponsors.Select(x => new SponsorDto()
            {
                Id = x.Id,
                Nombre = x.Nombre,
                Imagen = $"{ _configuration.GetSection("siteUrl").Value }/sponsor/image?name={ x.Imagen }"
            }).ToList();
        }

        public SponsorDto GetDetailById(int id)
        {
            var sponsor = _sponsorRepository.GetById(id);

            return new SponsorDto()
            {
                Id = sponsor.Id,
                Nombre = sponsor.Nombre,
                Imagen = $"{ _configuration.GetSection("siteUrl").Value }/sponsor/image?name={ sponsor.Imagen }"
            };
        }

        public ImageDto GetImage(string name)
        {
            return _imageService.GetImage(name, "Sponsors");
        }
        public void Update(SponsorDto sponsordto)
        {

            var spon = new Sponsor()
            {   Id = sponsordto.Id,
                Nombre = sponsordto.Nombre,
                Activo = sponsordto.Activo,
                Imagen = sponsordto.Imagen,
            };
            _sponsorRepository.Update(spon);

        }
        public void Delete(int id)
        {
            var sponsor = _sponsorRepository.GetById(id);
            sponsor.Activo = false;
            _sponsorRepository.Update(sponsor);
        }
    }
}
