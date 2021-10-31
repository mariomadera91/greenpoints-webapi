namespace GreenPoints.Services
{
    public interface IImageService
    {
        ImageUrlDto GetImage(string name, string entityName);
    }
}
