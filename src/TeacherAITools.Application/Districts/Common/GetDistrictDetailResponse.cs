namespace TeacherAITools.Application.Districts.Common
{
    public record GetDistrictDetailResponse(
        int DistrictId,
        string DistrictName,
        List<GetWardResponse> Wards);
}
