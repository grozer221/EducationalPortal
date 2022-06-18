using EducationalPortal.Business.Repositories;
using EducationalPortal.Business.Services;
using EducationalPortal.Portgres.Repositories;
using EducationalPortal.Portgres.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EducationalPortal.Portgres.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddPostgres(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(ServiceLifetime.Scoped);
            services.AddScoped<IBackupRepository, BackupRepository>();
            services.AddScoped<IEducationalYearRepository, EducationalYearRepository>();
            services.AddScoped<IFileRepository, FileRepository>();
            services.AddScoped<IGradeRepository, GradeRepository>();
            services.AddScoped<IHomeworkRepository, HomeworkRepository>();
            services.AddScoped<ISettingRepository, SettingRepository>();
            services.AddScoped<ISubjectPostRepository, SubjectPostRepository>();
            services.AddScoped<ISubjectRepository, SubjectRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddSingleton<IBackupService, BackupService>();
            //services.AddHostedService<BackupService>();
            return services;
        }
    }
}
