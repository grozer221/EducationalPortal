using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPortal.Business.Services
{
    public interface IBackupService
    {
        Task<string> BackupDatabase();
        Task RestoreDatabase(string backupFullPath);
    }
}
