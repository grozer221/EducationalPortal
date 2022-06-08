using EducationalPortal.Business.Models;
using EducationalPortal.Business.Repositories;
using EducationalPortal.MsSql.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace EducationalPortal.MsSql.Repositories
{
    public class BackupRepository : BaseRepository<BackupModel>, IBackupRepository
    {
        private readonly AppDbContext context;

        public BackupRepository(AppDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<string> BackupDatabase()
        {
            string database = context.Database.GetDbConnection().Database;
            string name = $"{DateTime.Now.ToString("dd.MM.yyyy_HH-mm-ss")}.bak";
            string fullPath = $@"{Environment.GetEnvironmentVariable("BACKUPS_FOLDER_PATH")}\{name}";
            await context.Database.ExecuteSqlRawAsync($@"BACKUP DATABASE [{database}] TO  DISK = N'{fullPath}'");
            return fullPath;
        }
        
        public Task RestoreDatabase(string backupFullPath)
        {
            return context.Database.ExecuteSqlRawAsync($@"RESTORE FILELISTONLY FROM DISK = '{backupFullPath}'");
        }
    }
}
