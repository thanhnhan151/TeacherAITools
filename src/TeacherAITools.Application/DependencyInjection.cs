using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TeacherAITools.Application.Common.Mappings;
using TeacherAITools.Application.Users.Commands.CreateUser;

namespace TeacherAITools.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfiles));

            services.AddMediatR(options =>
            {
                options.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);

                //options.AddOpenBehavior(typeof(AuthorizationBehavior<,>));
                //options.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });

            services.AddScoped<IValidator<CreateUserCommand>, CreateUserCommandValidator>();

            services.AddValidatorsFromAssemblyContaining(typeof(DependencyInjection));
            return services;
        }
    }
}
