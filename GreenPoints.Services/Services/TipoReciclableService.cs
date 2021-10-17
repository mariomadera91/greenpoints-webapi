using GreenPoints.Data;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

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
                Imagen = $"{ _configuration.GetSection("siteUrl").Value }/tipo-reciclable/image?name={ x.Imagen }"
            }).ToList();
        }

        public List<TipoReciclableDto> GetByPunto(int puntoId)
        {
            var lotes = _loteRepository.GetActiveByPunto(puntoId);
            return _tipoReciclableRepository.GetByPunto(puntoId)
                .Where(x=> lotes.Any(y => y.TipoId == x.Id ))
                .Select(x => new TipoReciclableDto()
            {
                Id = x.Id,
                Nombre = x.Nombre,
                Points = x.PuntosKg,
                Imagen = $"{ _configuration.GetSection("siteUrl").Value }/tipo-reciclable/image?name={ x.Imagen }"
            }).ToList();
        }

        public ImageDto GetImage(string name)
        {
            return _imageService.GetImage(name, "TiposReciclables");
        }
    }
}
