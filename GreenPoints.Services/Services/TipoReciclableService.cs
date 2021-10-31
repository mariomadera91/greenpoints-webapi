using GreenPoints.Data;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using GreenPoints.Domain;


namespace GreenPoints.Services
{
    public class TipoReciclableService : ITipoReciclableService
    {
        private ITipoReciclableRepository _tipoReciclableRepository;
        private ILoteRepository _loteRepository;
        private IImageService _imageService;
        private IConfiguration _configuration;

        public TipoReciclableService(ITipoReciclableRepository tipoReciclableRepository,
            ILoteRepository loteRepository,
            IImageService imageService,
            IConfiguration configuration)
        {
            _tipoReciclableRepository = tipoReciclableRepository;
            _loteRepository = loteRepository;
            _imageService = imageService;
            _configuration = configuration;
        }
        public List<TipoReciclableDto> Get()
        {
            return _tipoReciclableRepository.Get().Select(x => new TipoReciclableDto()
            {
                Id = x.Id,
                Nombre = x.Nombre,
                Points = x.PuntosKg,
                Activo = x.Activo,
                Imagen = $"{ _configuration.GetSection("siteUrl").Value }/tipo-reciclable/image?name={ x.Imagen }"
            }).ToList();
        }

        public List<TipoReciclableDto> GetByPunto(int puntoId, bool onlyOpenedLote)
        {
            var lotes = _loteRepository.GetActiveByPunto(puntoId);

            return _tipoReciclableRepository.GetByPunto(puntoId)
                .Where(x => !onlyOpenedLote || lotes.Any(y => y.TipoId == x.Id ))
                .Select(x => new TipoReciclableDto()
            {
                Id = x.Id,
                Nombre = x.Nombre,
                Points = x.PuntosKg,
                Activo = x.Activo,
                Imagen = $"{ _configuration.GetSection("siteUrl").Value }/tipo-reciclable/image?name={ x.Imagen }",
                HasActiveLote = lotes.Any(y => y.TipoId == x.Id)
            }).OrderBy(x => x.HasActiveLote).ToList();
        }

        public ImageUrlDto GetImage(string name)
        {
            return _imageService.GetImage(name, "TiposReciclables");
        }

        public void AddTipoReciclable(CreateTipoReciclableDto tipoReciclableDto)
        {
            var tipo = new TipoReciclable()
            {
                Nombre = tipoReciclableDto.Nombre,
                PuntosKg = tipoReciclableDto.Points,
                Activo = true
            };
            _tipoReciclableRepository.AddTipoReciclable(tipo);
        }

        public TipoReciclableDto GetDetailById(int id)
        {
            var tipo = _tipoReciclableRepository.GetById(id);

            return new TipoReciclableDto()
            {
                Id = tipo.Id,
                Nombre = tipo.Nombre,
                Points = tipo.PuntosKg,
                Activo = tipo.Activo,
                Imagen = $"{ _configuration.GetSection("siteUrl").Value }/tipo-reciclable/image?name={ tipo.Imagen }"
            };
        }
        public void Update(TipoReciclableDto tipoReciclableDto)
        {
            var tipo = new TipoReciclable()
            {
                Id = tipoReciclableDto.Id,
                Nombre = tipoReciclableDto.Nombre,
                PuntosKg = tipoReciclableDto.Points,
                Activo = true,
                Imagen = tipoReciclableDto.Imagen
            };
            _tipoReciclableRepository.Update(tipo);
        }

        public void Delete(int id)
        {
            var tipo = _tipoReciclableRepository.GetById(id);
            tipo.Activo = false;
            _tipoReciclableRepository.Update(tipo);
        }
    }
}
