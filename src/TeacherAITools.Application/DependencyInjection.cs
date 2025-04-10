using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TeacherAITools.Application.Blogs.Commands.CreateBlog;
using TeacherAITools.Application.Blogs.Commands.UpdateBlog;
using TeacherAITools.Application.Blogs.Common;
using TeacherAITools.Application.Common.Mappings;
using TeacherAITools.Application.Schools.Commands.CreateSchool;
using TeacherAITools.Application.Schools.Commands.UpdateSchool;
using TeacherAITools.Application.Schools.Common;
using TeacherAITools.Application.Users.Commands.ChangePassword;
using TeacherAITools.Application.Users.Commands.CreateUser;
using TeacherAITools.Application.Users.Commands.UpdateUser;
using TeacherAITools.Application.Users.Common;

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

            #region Validators
            services.AddScoped<IValidator<CreateUserCommand>, CreateUserCommandValidator>();
            services.AddScoped<IValidator<UpdateUserRequest>, UpdateUserCommandValidator>();
            services.AddScoped<IValidator<CreateSchoolCommand>, CreateSchoolCommandValidator>();
            services.AddScoped<IValidator<UpdateSchoolRequest>, UpdateSchoolCommandValidator>();
            services.AddScoped<IValidator<CreateBlogCommand>, CreateBlogCommandValidator>();
            services.AddScoped<IValidator<UpdateBlogRequest>, UpdateBlogCommandValidator>();
            services.AddScoped<IValidator<ChangePasswordCommand>, ChangePasswordCommandValidator>();
            #endregion

            services.AddValidatorsFromAssemblyContaining(typeof(DependencyInjection));
            return services;
        }
    }
}
