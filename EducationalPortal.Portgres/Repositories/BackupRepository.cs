using EducationalPortal.Business.Models;
using EducationalPortal.Business.Repositories;
using EducationalPortal.Portgres.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Net;

namespace EducationalPortal.Portgres.Repositories
{
    public class BackupRepository : BaseRepository<BackupModel>, IBackupRepository
    {
        private readonly AppDbContext context;
        public BackupRepository(AppDbContext context) : base(context)
        {
            this.context = context;
        }

        public Task<string> BackupDatabase()
        {
            var backupsDirectory = Path.Combine(Environment.CurrentDirectory, "Backups");
            if (!Directory.Exists(backupsDirectory))
                Directory.CreateDirectory(backupsDirectory);

            var date = DateTime.Now.ToString("dd.MM.yyyy_HH-mm-ss-fff");
            var outputFileFullPath = Path.Combine(backupsDirectory, $"{date}.bak");
            string connectionString = AppDbContext.GetConnectionString();
            string database = connectionString.Split(";").First(p => p.StartsWith("Database")).Replace("Database=", string.Empty);
            string username = connectionString.Split(";").First(p => p.StartsWith("Username")).Replace("Username=", string.Empty);
            var command = $@"pg_dump -c -C -U {username} -d {database} > ""{outputFileFullPath}""";
            DatabaseAction(Environment.GetEnvironmentVariable("PG_PATH"), command);
            return Task.FromResult(outputFileFullPath);
        }
        
        public Task RestoreDatabase(string backupPath)
        {
            string connectionString = AppDbContext.GetConnectionString();
            string database = connectionString.Split(";").First(p => p.StartsWith("Database")).Replace("Database=", string.Empty);
            string username = connectionString.Split(";").First(p => p.StartsWith("Username")).Replace("Username=", string.Empty);
            //var command = $"pg_restore -c -C -d {database} -U {username} -C {backupUrl}";
            var command = $@"psql -U {username} -d {database} < ""{backupPath}""";
            DatabaseAction(Environment.GetEnvironmentVariable("PG_PATH"), command);
            return Task.CompletedTask;
        }

        private static void DatabaseAction(string postgresqlPath, string command)
        {
            var process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.CreateNoWindow = false;
            process.StartInfo.WorkingDirectory = postgresqlPath;
            process.StartInfo.Arguments = $"/C {command}";
            process.Start();
            process.WaitForExit();
            process.Close();
        }
    }
}