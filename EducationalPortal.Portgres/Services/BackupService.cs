using EducationalPortal.Business.Services;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPortal.Portgres.Services
{
    public class BackupService : IBackupService, IHostedService
    {
        public Task StartAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> BackupDatabase()
        {
            var backupsDirectory = Path.Combine(Environment.CurrentDirectory, "Backups");
            if (!Directory.Exists(backupsDirectory))
                Directory.CreateDirectory(backupsDirectory);

            var date = DateTime.Now.ToString("dd.MM.yyyy_HH-mm-ss-fff");
            var outputFileFullPath = Path.Combine(backupsDirectory, $"{date}.sql");
            string connectionString = AppDbContext.GetConnectionString();
            string database = connectionString.Split(";").First(p => p.StartsWith("Database")).Replace("Database=", string.Empty);
            string username = connectionString.Split(";").First(p => p.StartsWith("Username")).Replace("Username=", string.Empty);
            var command = $"pg_dump -U {username} -Fc {database} > {outputFileFullPath}";
            DatabaseAction(Environment.GetEnvironmentVariable("PG_PATH"), command);
            return Task.FromResult(outputFileFullPath);
        }

        public Task RestoreDatabase(string backupUrl)
        {
            string connectionString = AppDbContext.GetConnectionString();
            string database = connectionString.Split(";").First(p => p.StartsWith("Database")).Replace("Database=", string.Empty);
            string username = connectionString.Split(";").First(p => p.StartsWith("Username")).Replace("Username=", string.Empty);
            var command = $"pg_restore -c -d {database} -U {username} -C {backupUrl}";
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
