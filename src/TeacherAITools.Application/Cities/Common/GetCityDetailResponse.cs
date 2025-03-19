using TeacherAITools.Application.Districts.Common;

namespace TeacherAITools.Application.Cities.Common
{
    public record GetCityDetailResponse(
        int CityId,
        string CityName,
        List<GetDistrictResponse> Districts);
}
