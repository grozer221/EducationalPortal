using EducationalPortal.Business.Repositories;
using EducationalPortal.PostgreSql.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace EducationalPortal.PostgreSql.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddPostgreSql(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(ServiceLifetime.Transient);
            services.AddTransient<IEducationalYearRepository, EducationalYearRepository>();
            services.AddTransient<IGradeRepository, GradeRepository>();
            services.AddTransient<IHomeworkRepository, HomeworkRepository>();
            services.AddTransient<ISettingRepository, SettingRepository>();
            services.AddTransient<ISubjectPostRepository, SubjectPostRepository>();
            services.AddTransient<ISubjectRepository, SubjectRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            return services;
        }
    }
}
