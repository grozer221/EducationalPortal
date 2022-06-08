using EducationalPortal.Business.Abstractions;
using EducationalPortal.Business.Models;

namespace EducationalPortal.Business.Repositories
{
    public interface IBackupRepository : IBaseRepository<BackupModel>
    {
        Task<string> BackupDatabase();
        Task RestoreDatabase(string backupFullPath);
    }
}
