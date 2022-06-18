﻿using EducationalPortal.Business.Repositories;
using EducationalPortal.Portgres.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace EducationalPortal.Portgres.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddPostgres(this IServiceCollection services)
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