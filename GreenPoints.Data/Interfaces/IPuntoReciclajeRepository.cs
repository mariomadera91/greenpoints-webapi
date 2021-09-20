using GreenPoints.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenPoints.Data
{
    public interface IPuntoReciclajeRepository
    {
        PuntoReciclaje GetPuntoReciclaje(int id);
        void Add(PuntoReciclaje punto);
    }
}
