using AutoMapper;
using WebApplication1.Models.DTO;

namespace WebApplication1.Models.Profiles
{
    public class PetProfile: Profile
    {
        public PetProfile() 
        { 
            CreateMap<Pet,PetDTO>();
            CreateMap<PetDTO, Pet>();

        }
      }
}
