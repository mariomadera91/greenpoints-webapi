using System.IO;

namespace GreenPoints.Services
{
    public class ImageDto
    {
        public FileStream Image { get; set; }
        public string ContentType { get; set; }
    }
}
