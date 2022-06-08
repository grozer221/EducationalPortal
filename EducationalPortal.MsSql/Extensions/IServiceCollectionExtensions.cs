using EducationalPortal.Business.Repositories;
using EducationalPortal.MsSql.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace EducationalPortal.MsSql.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddMsSql(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(ServiceLifetime.Transient);
            services.AddTransient<IBackupRepository, BackupRepository>();
            services.AddTransient<IEducationalYearRepository, EducationalYearRepository>();
            services.AddTransient<IFileRepository, FileRepository>();
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
