using GreenPoints.Domain;
using System;
using System.Collections.Generic;

namespace GreenPoints.Data

{
    public interface ISponsorRepository
    { 
        void AddSponsor(Sponsor sponsor);
        List<Sponsor> Get();
        public Sponsor GetById(int id);
        public void Update(Sponsor sponsor);
        List<Sponsor> GetTop();
    }
}
