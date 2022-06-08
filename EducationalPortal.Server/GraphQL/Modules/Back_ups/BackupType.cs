using EducationalPortal.Business.Models;
using EducationalPortal.Business.Repositories;
using EducationalPortal.Server.GraphQL.Abstraction;
using EducationalPortal.Server.GraphQL.Modules.Files;

namespace EducationalPortal.Server.GraphQL.Modules.Back_ups
{
    public class BackupType : BaseType<BackupModel>
    {
        public BackupType() : base()
        {
            Field<FileType, FileModel>()
                .Name("File")
                .ResolveAsync(async context =>
                {
                    var fileRepository = context.RequestServices.GetRequiredService<IFileRepository>();
                    return await fileRepository.GetByIdOrDefaultAsync(context.Source.FileId);
                });
        }
    }
}
