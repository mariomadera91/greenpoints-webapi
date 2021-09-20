﻿using GreenPoints.Domain;
using System.Collections.Generic;

namespace GreenPoints.Data
{
    public interface IIntercambioRepository
    {
        List<Intercambio> GetBySocio(int socioId);
        Intercambio GetById(int id);
    }
}