namespace GreenPoints.Services
{
    public interface IImageService
    {
        ImageDto GetImage(string name, string entityName);
    }
}
