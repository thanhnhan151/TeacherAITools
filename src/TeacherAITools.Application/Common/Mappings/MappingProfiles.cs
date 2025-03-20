using AutoMapper;
using TeacherAITools.Application.Cities.Common;
using TeacherAITools.Application.Districts.Common;
using TeacherAITools.Application.Users.Common;
using TeacherAITools.Domain.Entities;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Common.Mappings
{
    internal class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            #region City
            CreateMap<City, GetCityResponse>();
            CreateMap<City, GetCityDetailResponse>();
            #endregion

            #region District
            CreateMap<District, GetDistrictResponse>();
            CreateMap<District, GetDistrictDetailResponse>();
            #endregion

            #region Ward
            CreateMap<Ward, GetWardResponse>();
            #endregion

            #region User
            CreateMap<User, GetUserResponse>()
                .ForMember(u => u.School, u => u.MapFrom(u => u.School.Name))
                .ForMember(u => u.Manager, u => u.MapFrom(u => u.ManagerId != null ? u.Manager.Fullname : "N/A"))
                .ForMember(u => u.Role, u => u.MapFrom(u => u.Role.RoleName));
            CreateMap<PaginatedList<User>, PaginatedList<GetUserResponse>>();
            #endregion
        }
    }
}
