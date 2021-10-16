using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace GreenPoints.Services
{
    public class ImageService : IImageService
    {
        private IConfiguration _configuration;

        public ImageService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ImageDto GetImage(string name, string entityName)
        {
            var basePath = _configuration.GetSection("imagePath").Value;
            var imagePath = $"{ basePath }\\{ entityName }\\{ name }";
            var contentType = name.Contains(".") ? $"image/{ name.Split(".")[1] }" : "image/jpg";

            if (!Directory.Exists(basePath) || string.IsNullOrEmpty(name) || !File.Exists(imagePath))
            {
                return new ImageDto()
                {
                    ContentType = "image/png",
                    Image = File.OpenRead($"{ AppDomain.CurrentDomain.BaseDirectory }\\Content\\SinImagen.png")
                };
            }

            return new ImageDto()
            {
                Image = File.OpenRead(imagePath),
                ContentType = contentType
            };
        }
    }
}
