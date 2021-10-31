using System.IO;

namespace GreenPoints.Services
{
    public class ImageUrlDto
    {
        public FileStream Image { get; set; }
        public string ContentType { get; set; }
    }
}
