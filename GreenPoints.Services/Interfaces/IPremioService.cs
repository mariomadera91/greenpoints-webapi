﻿using System.Collections.Generic;

namespace GreenPoints.Services
{
    public interface IPremioService
    {
        List<PremioListDto> Get(bool admin = false);

        PremioDto GetDetailById(int id, bool admin);

        ImageUrlDto GetImage(string name);

        List<PremioListDto> GetTop();

        List<SocioPremioListDto> GetSocioPremioBySocio(int socioId);

        SocioPremioDto GetSocioPremio(int socioPremioId);

        void Post(CreatePremioDto premioDto);

        void Put(PremioDto premioDto);

        void Delete(int id);
    }
}
