using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using GreenPoints.Data;
using System.IO;
using System;
using System.Transactions;
using GreenPoints.Domain;

namespace GreenPoints.Services
{
    public class PremioService : IPremioService
    {
        private IPremioRepository _premioRepository;
        private IImageService _imageService;
        private ISponsorRepository _sponsorRepository;
        private IConfiguration _configuration;

        public PremioService(
            IPremioRepository premioRepository,
            IImageService imageService,
            ISponsorRepository sponsorRepository,
            IConfiguration configuration)
        {
            _premioRepository = premioRepository;
            _imageService = imageService;
            _sponsorRepository = sponsorRepository;
            _configuration = configuration;
        }

        public List<PremioListDto> Get()
        {
            var premios = _premioRepository.Get();

            return premios.OrderBy(x => x.Puntos).Select(x => new PremioListDto()
            {
                Id = x.Id,
                Nombre = x.Nombre,
                Points = x.Puntos.ToString(),
                SponsorName = x.Sponsor.Nombre,
                Imagen = $"{ _configuration.GetSection("siteUrl").Value }/premio/image?name={ x.Imagen }"
            }).ToList();
        }

        public PremioDto GetDetailById(int id, bool admin)
        {
            var premio = _premioRepository.GetById(id);

            List<string> codigos = null;

            if (admin)
            {
                codigos = _premioRepository.GetPremioCodigos(id).Select(x => x.Codigo).ToList();
            }

            return new PremioDto()
            {
                Id = premio.Id,
                Name = premio.Nombre,
                Description = premio.Descripcion,
                SponsorId = premio.SponsorId,
                Desde = premio.VigenciaDesde,
                Hasta = premio.VigenciaHasta,
                Puntos = premio.Puntos,
                Observacion = premio.Observacion,
                Imagen = $"{ _configuration.GetSection("siteUrl").Value }/premio/image?name={ premio.Imagen }",
                Codigos = codigos
            };
        }

        public ImageUrlDto GetImage(string name)
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
            byte[] bytes = (premioDto.Image != null && premioDto.Image.base64 != null) ? Convert.FromBase64String(premioDto.Image.base64) : null;
            var imageFileName = (premioDto.Image != null && premioDto.Image.base64 != null) ? Guid.NewGuid() + ".png" : string.Empty;
            var path = $"{ _configuration.GetSection("imagePath").Value }\\premios\\{ imageFileName }";
            var sponsor = _sponsorRepository.GetById(premioDto.SponsorId);

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
                premio.VigenciaHasta = !string.IsNullOrEmpty(premioDto.FechaVto) ? DateTime.ParseExact(premioDto.FechaVto, "dd-MM-yyyy", null) : null;
                premio.SponsorId = premioDto.SponsorId;
                premio.Imagen = !string.IsNullOrEmpty(imageFileName) ? imageFileName : sponsor.Imagen;
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

                if(!string.IsNullOrEmpty(imageFileName))
                {
                    File.WriteAllBytes(path, bytes);
                }
                
                scope.Complete();
            }
            
        }

        public void Put(PremioDto premioDto)
        {
            var premio = _premioRepository.GetById(premioDto.Id);
            var premioCodigos = _premioRepository.GetPremioCodigos(premioDto.Id);

            premio.Nombre = premioDto.Name;
            premio.Observacion = premioDto.Observacion;
            premio.Descripcion = premioDto.Description;
            premio.VigenciaDesde = premioDto.Desde;
            premio.VigenciaHasta = premioDto.Hasta;
            premio.SponsorId = premioDto.SponsorId;
            premio.Stock = premioDto.Codigos.Count();
            premio.Puntos = premioDto.Puntos;

            byte[] bytes = (premioDto.ImageData != null && premioDto.ImageData.base64 != null) ? Convert.FromBase64String(premioDto.ImageData.base64) : null;
            var imageFileName = (premioDto.ImageData != null && premioDto.ImageData.base64 != null) ? Guid.NewGuid() + ".png" : string.Empty;
            var path = $"{ _configuration.GetSection("imagePath").Value }\\premios\\";

            premio.Imagen = !string.IsNullOrEmpty(imageFileName) ? imageFileName : premio.Imagen;

            using (var scope = new TransactionScope())
            {
                var premioCodigosToAdd = premioDto.Codigos.Where(x => !premioCodigos.Any(y => y.Codigo == x))
                                            .Select(x => new PremioCodigo()
                                            {
                                                Activo = true,
                                                Codigo = x,
                                                PremioId = premioDto.Id
                                            }).ToList();

                var premioCodigosToDelete = premioCodigos.Where(x => !premioDto.Codigos.Any(y => y == x.Codigo)).ToList();


                _premioRepository.CreatePremioCodigos(premioCodigosToAdd);
                _premioRepository.DisablePremioCodigos(premioCodigosToDelete);
                _premioRepository.Update(premio);

                if (!string.IsNullOrEmpty(imageFileName))
                {
                    File.WriteAllBytes(path + imageFileName, bytes);
                }

                scope.Complete();
            }
            
        }

        public void Delete(int id)
        {
            var premio = _premioRepository.GetById(id);
            var premioCodigos = _premioRepository.GetPremioCodigos(id);

            using (var scope = new TransactionScope())
            {
                _premioRepository.DisablePremioCodigos(premioCodigos);

                premio.Activo = false;

                _premioRepository.Update(premio);
                scope.Complete();
            }
        }

    }
}
