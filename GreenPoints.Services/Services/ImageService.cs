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

            if (!Directory.Exists(basePath) || string.IsNullOrEmpty(name))
            {
                return new ImageDto()
                {
                    ContentType = "image/png",
                    Image = File.OpenRead($"{ AppDomain.CurrentDomain.BaseDirectory }\\Content\\SinImagen.png")
                };
            }

            var imagePath = $"{ basePath }\\{ entityName }\\{ name }";
            var contentType = name.Contains(".") ? $"image/{ name.Split(".")[1] }" : "image/jpg";

            return new ImageDto()
            {
                Image = File.OpenRead(imagePath),
                ContentType = contentType
            };
        }
    }
}
