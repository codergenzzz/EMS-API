using EMS_API.Dtos;
using EMS_API.Models;
namespace EMS_API.Helpers
{
    public class MappingProfile : AutoMapper.Profile
    {
        public MappingProfile()
        {
            CreateMap<Device, DeviceDto>().ReverseMap();
            CreateMap<Log, LogDto>().ReverseMap();
            CreateMap<Role, RoleDto>().ReverseMap();
            CreateMap<Profile, ProfileDto>().ReverseMap();

        }
    }
}
