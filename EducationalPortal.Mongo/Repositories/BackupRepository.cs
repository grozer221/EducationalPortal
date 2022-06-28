using EducationalPortal.Business.Models;
using EducationalPortal.Business.Repositories;
using EducationalPortal.Mongo.Abstractions;

namespace EducationalPortal.Mongo.Repositories
{
    public class BackupRepository : IBackupRepository
    {
        public async Task<string> BackupDatabase()
        {
            throw new NotImplementedException();
            //string database = context.Database.GetDbConnection().Database;
            //string name = $"{DateTime.Now.ToString("dd.MM.yyyy_HH-mm-ss-fff")}.bak";
            //string fullPath = $@"{Environment.GetEnvironmentVariable("BACKUPS_FOLDER_PATH")}\{name}";
            //await context.Database.ExecuteSqlRawAsync($@"
            //    BACKUP DATABASE [{database}]
            //    TO DISK = '{fullPath}' WITH INIT");
            //return fullPath;
        }

        public Task RestoreDatabase(string backupFullPath)
        {
            throw new NotImplementedException();
            //string database = context.Database.GetDbConnection().Database;
            //return context.Database.ExecuteSqlRawAsync($@"
            //    USE master
            //    ALTER DATABASE [{database}]
            //    SET SINGLE_USER
            //    --This rolls back all uncommitted transactions in the db.
            //    WITH ROLLBACK IMMEDIATE
            //    RESTORE DATABASE {database}
            //    FROM DISK = N'{backupFullPath}'
            //    WITH RECOVERY, REPLACE");
        }
    }
}
