using AutoMapper;
using TeacherAITools.Application.Cities.Common;
using TeacherAITools.Application.Districts.Common;
using TeacherAITools.Domain.Entities;

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
        }
    }
}
