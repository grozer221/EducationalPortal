using EducationalPortal.Business.Models;
using EducationalPortal.Business.Repositories;
using EducationalPortal.Server.GraphQL.Abstraction;
using EducationalPortal.Server.GraphQL.Modules.Files;

namespace EducationalPortal.Server.GraphQL.Modules.Back_ups
{
    public class BackupType : BaseType<BackupModel>
    {
        public BackupType(IServiceProvider serviceProvider) : base()
        {
            Field<FileType, FileModel>()
                .Name("File")
                .ResolveAsync(async context =>
                {
                    using var scope = serviceProvider.CreateScope();
                    var fileRepository = scope.ServiceProvider.GetRequiredService<IFileRepository>();
                    return await fileRepository.GetByIdOrDefaultAsync(context.Source.FileId);
                });
        }
    }
}
