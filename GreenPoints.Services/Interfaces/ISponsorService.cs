using GreenPoints.Domain;
using System.Collections.Generic;

namespace GreenPoints.Services.Interfaces
{
    public interface ISponsorService
    {
        void AddSponsor(CreateSponsorDto createSponsorDto);
        
        List<SponsorDto> Get();
        
        SponsorDto GetDetailById(int id);

        void Update(SponsorDto sponsorDto);

        void Delete(int id);
        ImageUrlDto GetImage(string name);
    }
}
