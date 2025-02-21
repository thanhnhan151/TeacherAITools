using TeacherAITools.Api;
using TeacherAITools.Api.Startups;
using TeacherAITools.Application;
using TeacherAITools.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddPresentation()
        .AddInfrastructure(builder.Configuration)
        .AddApplication();
}

var app = builder.Build();
{
    app.ConfigureSwagger();

    app.UseHttpsRedirection();

    app.UseAuthentication();

    app.UseCors("AllowOrigin");

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}