using EducationalPortal.Business.Models;
using EducationalPortal.Business.Repositories;
using EducationalPortal.Server.Extensions;
using EducationalPortal.Server.GraphQL.Abstraction;
using EducationalPortal.Server.GraphQL.Modules.Auth;
using EducationalPortal.Server.Services;
using GraphQL;
using GraphQL.Types;
using System.Net;

namespace EducationalPortal.Server.GraphQL.Modules.Back_ups
{
    public class BackupsMutations : ObjectGraphType, IMutationMarker
    {
        public BackupsMutations(IBackupRepository backupRepository, CloudinaryService cloudinaryService, IFileRepository fileRepository, IHttpContextAccessor httpContextAccessor)
        {
            Field<NonNullGraphType<BackupType>, BackupModel>()
                .Name("CreateBackup")
                .ResolveAsync(async context =>
                {
                    string backupFullPath = await backupRepository.BackupDatabase();
                    using var stream = new MemoryStream(File.ReadAllBytes(backupFullPath).ToArray());
                    string backupName = Path.GetFileName(backupFullPath);
                    FormFile formFile = new FormFile(stream, 0, stream.Length, backupName, backupName);
                    string urlPath = await cloudinaryService.UploadFileAsync(formFile, false);
                    Guid currentUserId = httpContextAccessor.HttpContext.GetUserId();
                    var file = await fileRepository.CreateAsync(new FileModel
                    {
                        Name = backupName,
                        Path = urlPath,
                        CreatorId = currentUserId,
                    });
                    File.Delete(backupFullPath);
                    return await backupRepository.CreateAsync(new BackupModel
                    {
                        FileId = file.Id,
                    }); 
                })
                .AuthorizeWith(AuthPolicies.Administrator);
            
            Field<NonNullGraphType<BackupType>, BackupModel>()
                .Name("RestoreBackup")
                .Argument<NonNullGraphType<GuidGraphType>, Guid>("Id", "Argument for Restore Backup")
                .ResolveAsync(async context =>
                {
                    Guid id = context.GetArgument<Guid>("Id");
                    var backup = await backupRepository.GetByIdAsync(id, b => b.File);
                    string backupFullPath = $@"{Environment.GetEnvironmentVariable("BACKUPS_FOLDER_PATH")}\{backup.File.Name}";
                    var webClient = new WebClient();
                    webClient.DownloadFileAsync(new Uri(backup.File.Path), backupFullPath);
                    await backupRepository.RestoreDatabase(backupFullPath);
                    File.Delete(backupFullPath);
                    return backup;
                })
                .AuthorizeWith(AuthPolicies.Administrator);

            Field<NonNullGraphType<BooleanGraphType>, bool>()
               .Name("RemoveBackup")
               .Argument<NonNullGraphType<IdGraphType>, Guid>("Id", "Argument for remove Backup")
               .ResolveAsync(async context =>
               {
                   Guid id = context.GetArgument<Guid>("Id");
                   await backupRepository.RemoveAsync(id);
                   return true;
               })
               .AuthorizeWith(AuthPolicies.Administrator);
        }
    }
}
