using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;

namespace GreenPoints.Services
{
    public static class AddressHelper
    {
        public static string GetAddress(decimal latitude, decimal longitude)
        {
            var webClient = new WebClient();
            webClient.Headers.Add("User-Agent: Other");

            var latitud = latitude.ToString().PadRight(12, '0').Replace(',', '.');
            var longitud = longitude.ToString().PadRight(12, '0').Replace(',', '.');

            var addressUrl = $"http://nominatim.openstreetmap.org/reverse?format=json&lat={ latitud }&lon={ longitud }";
            var jsonData = webClient.DownloadData(addressUrl);

            var ser = new DataContractJsonSerializer(typeof(RootAddress));
            var address = ((RootAddress)ser.ReadObject(new MemoryStream(jsonData)));

            return (address != null && address.address != null && address.address.road != null) ? 
                        address.address.road + " " + address.address.house_number : "";
        }
    }
}
