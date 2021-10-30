﻿using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using GreenPoints.Data;
using System.IO;
using System;
using static System.Net.Mime.MediaTypeNames;
using System.Transactions;
using GreenPoints.Domain;

namespace GreenPoints.Services
{
    public class PremioService : IPremioService
    {
        private IPremioRepository _premioRepository;
        private IImageService _imageService;
        private IConfiguration _configuration;

        public PremioService(
            IPremioRepository premioRepository,
            IImageService imageService,
            IConfiguration configuration)
        {
            _premioRepository = premioRepository;
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

        public void Post(CreatePremioDto premioDto)
        {
            byte[] bytes = Convert.FromBase64String(premioDto.Image.base64);
            var imageFileName = Guid.NewGuid() + ".png";
            var path = $"{ _configuration.GetSection("imagePath").Value }\\premios\\{ imageFileName }";

            using (var scope = new TransactionScope())
            {
                var premio = new Premio();

                premio.Nombre = premioDto.Nombre;
                premio.Descripcion = premioDto.Descripcion;
                premio.Observacion = (premioDto.Observacion != null) ? 
                                                    premioDto.Observacion : 
                                                    _configuration.GetSection("Premio:defaultObservation").Value;
                premio.Activo = true;
                premio.Fecha = DateTime.Now;
                premio.VigenciaDesde = DateTime.ParseExact(premioDto.FechaInicio, "dd-MM-yyyy",null);
                premio.VigenciaHasta = DateTime.ParseExact(premioDto.FechaVto, "dd-MM-yyyy", null);
                premio.SponsorId = 2;
                premio.Imagen = imageFileName;
                premio.Puntos = premioDto.Puntos;
                premio.Stock = premioDto.Codigos.Count;

                _premioRepository.CreatePremio(premio);

                if (premioDto.Codigos != null)
                {
                    var premioCodigos = new List<PremioCodigo>();

                    foreach (var codigo in premioDto.Codigos)
                    {
                        var premioCodigo = new PremioCodigo();
                        premioCodigo.Activo = true;
                        premioCodigo.PremioId = premio.Id;
                        premioCodigo.Codigo = codigo;
                        premioCodigos.Add(premioCodigo);
                    }

                    _premioRepository.CreatePremioCodigos(premioCodigos);
                }


                File.WriteAllBytes(path, bytes);
                scope.Complete();
            }
            
        }

    }
}
