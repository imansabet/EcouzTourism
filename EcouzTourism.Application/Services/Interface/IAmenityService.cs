using EcouzTourism.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcouzTourism.Application.Services.Interface
{
    public interface IAmenityService
    {
        IEnumerable<Amenity> GetAllAmenities();
        void CreateAmenity(Amenity amenity);
        void UpdateAmenity(Amenity amenity);
        Amenity GetAmenityById(int id);
        bool DeleteAmenity(int id);
    }
}
