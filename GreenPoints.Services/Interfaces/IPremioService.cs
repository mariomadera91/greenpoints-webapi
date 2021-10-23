﻿using System.Collections.Generic;

namespace GreenPoints.Services
{
    public interface IPremioService
    {
        List<PremioListDto> Get();

        PremioDto GetDetailById(int id);

        ImageDto GetImage(string name);

        List<PremioListDto> GetTop();

        List<SocioPremioListDto> GetSocioPremioBySocio(int socioId);

        SocioPremioDto GetSocioPremio(int socioPremioId);
    }
}
