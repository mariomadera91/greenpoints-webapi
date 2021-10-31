using GreenPoints.Data;
using GreenPoints.Domain;
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace GreenPoints.Services
{
    public class PlantaService : IPlantaService
    {
        private readonly IPlantaRepository _plantaRepository;
        private IImageService _imageService;
        private IConfiguration _configuration;
        public PlantaService(IPlantaRepository plantaRepository,
                              IImageService imageService,
                              IConfiguration configuration)
        {
            _plantaRepository = plantaRepository;
            _configuration = configuration;
            _imageService = imageService;
        }
        public List<PlantaSearchDto> GetSearch()
        {
            return _plantaRepository.Search().Select(x => new PlantaSearchDto()
            {
                Id = x.Id,
                Nombre = x.Nombre,
                Direccion = x.Direccion
            }).ToList();
        }
        public void AddPlanta(CreatePlantaDto plantaDto)
        {
            var plan = new Planta()
            {
                Nombre = plantaDto.Nombre,
                FechaCrea = DateTime.Now,
                Direccion = plantaDto.Direccion,
                Descripcion = plantaDto.Descripcion
            };
            _plantaRepository.AddPlanta(plan);
        }

        public List<PlantaDto> Get()
        {
            var plantas = _plantaRepository.Get();

            return plantas.Select(x => new PlantaDto()
            {
                Id = x.Id,
                Nombre = x.Nombre,
                Direccion = x.Direccion,
                Descripcion = x.Descripcion,
                Imagen = $"{ _configuration.GetSection("siteUrl").Value }/planta/image?name={ x.Imagen }"
            }).ToList();
        }

        public PlantaDto GetDetailById(int id)
        {
            var planta = _plantaRepository.GetById(id);

            return new PlantaDto()
            {
                Id = planta.Id,
                Nombre = planta.Nombre,
                Direccion = planta.Direccion,
                Descripcion = planta.Descripcion,
                Imagen = $"{ _configuration.GetSection("siteUrl").Value }/planta/image?name={ planta.Imagen }"
            };
        }

        public ImageUrlDto GetImage(string name)
        {
            return _imageService.GetImage(name, "Plantas");
        }
        public void Update(PlantaDto plantaDto)
        {
            var planta = _plantaRepository.GetById(plantaDto.Id);

            var plant = new Planta()
            {
                Id = plantaDto.Id,
                Nombre = plantaDto.Nombre,
                Direccion = plantaDto.Direccion,
                Descripcion = plantaDto.Descripcion,
                Imagen = plantaDto.Imagen,
                FechaCrea = planta.FechaCrea
            };
            _plantaRepository.Update(plant);
        }

        public void Delete(int id)
        {
            var planta = _plantaRepository.GetById(id);
            planta.Activo = false;
            _plantaRepository.Update(planta);
        }
    }
}
