using GreenPoints.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenPoints.Services
{
    public interface ISocioRecicladorService
    {
        void Create(CreateSocioRecicladorDto socioDto);
    }
}
