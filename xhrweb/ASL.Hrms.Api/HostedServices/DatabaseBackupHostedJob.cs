using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Attendance.Application.AttendanceProcessedData.Commands.Commands.V1;

namespace ASL.Hrms.Api.HostedServices
{
    public class DatabaseBackupHostedJob : CronJobService
    {
        private readonly ILogger<DatabaseBackupHostedJob> _logger;
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;
        public DatabaseBackupHostedJob(IScheduleConfig<DatabaseBackupHostedJob> config, ILogger<DatabaseBackupHostedJob> logger, IMediator mediator, IConfiguration configuration)
            : base(config.CronExpression, config.TimeZoneInfo)
        {
            _logger = logger;
            _mediator = mediator;
            _configuration = configuration;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("DatabaseBackupHostedJob starts.");
            return base.StartAsync(cancellationToken);
        }

        public override async Task DoWork(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{DateTime.Now:hh:mm:ss} DatabaseBackupHostedJob is satrting.");
            try
            {
                var connectionString = _configuration.GetConnectionString("HrmsDatabase");
                var _host = GetValueFromString(connectionString, "Server", ";");
                var _port = GetValueFromString(connectionString, "Port", ";");
                var _database = GetValueFromString(connectionString, "Database", ";");

                var _userId = GetValueFromString(connectionString, "User Id", ";");
                var _password = GetValueFromString(connectionString, "Password", ";");
                var _pgDumpPath = _configuration["HostedJobService:DatabaseBackupService:PgDumpPath"];
                var _databaseBackupDirectory = _configuration["HostedJobService:DatabaseBackupService:DatabaseBackupDirectory"];

                PostgreSqlDump(_pgDumpPath //pgDumpPath
                                    //, "dev_HrmsDB_" //outFile
                                    , _databaseBackupDirectory, //outDir
                                    _host,  //host
                                    _port, //port
                                    _database, //database
                                    _userId, //user
                                    _password // password 
                         );
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now:hh:mm:ss} DatabaseBackupHostedJob got exception. {ex.Message}");
            }
            _logger.LogInformation($"{DateTime.Now:hh:mm:ss} DatabaseBackupHostedJob is ended.");
            await Task.CompletedTask;
        }

        private string PostgreSqlDump(
           string pgDumpPath,
           //string outFile,
           string backupdir,
           string host,
           string port,
           string database,
           string user,
           string password)
        {

           // string backupFile = $"{backupdir} {database}_{DateTime.Now.ToString("yyyyMMdd")}.tar";
            string backupFile = $"{backupdir} {database}.tar";
            String dumpCommand = "\"" + pgDumpPath + "\\pg_dump.exe" + "\"" + " -h " + host + " -p " + port + " -d " + database + " -U " + user + " -F t";
            String passFileContent = "" + host + ":" + port + ":" + database + ":" + user + ":" + password + "";

            String batFilePath = Path.Combine(
                Path.GetTempPath(),
                Guid.NewGuid().ToString() + ".bat");

            String passFilePath = Path.Combine(
                Path.GetTempPath(),
                Guid.NewGuid().ToString() + ".conf");

            try
            {
                String batchContent = "";
                batchContent += "@" + "set PGPASSFILE=" + passFilePath + "\n";
                batchContent += "@" + dumpCommand + "  > " + "\"" + backupFile + "\"" + "\n";

                File.WriteAllText(
                    batFilePath,
                    batchContent,
                    Encoding.ASCII);

                File.WriteAllText(
                    passFilePath,
                    passFileContent,
                    Encoding.ASCII);

                if (File.Exists(backupFile))
                    File.Delete(backupFile);

                ProcessStartInfo oInfo = new ProcessStartInfo(batFilePath);
                oInfo.UseShellExecute = false;
                oInfo.CreateNoWindow = true;

                using (Process proc = System.Diagnostics.Process.Start(oInfo))
                {
                    proc.WaitForExit();
                    proc.Close();
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                if (File.Exists(batFilePath))
                    File.Delete(batFilePath);

                if (File.Exists(passFilePath))
                    File.Delete(passFilePath);
            }
            return "success";
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("DatabaseBackupHostedJob is stopping.");
            return base.StopAsync(cancellationToken);
        }

        private string GetValueFromString(string _content, string _keyword, string _separator)
        {
            int start = _content.ToLower().IndexOf(_keyword.ToLower());
            start = start + (_keyword).Length + 1;
            int end = _content.IndexOf(_separator, start);
            end = end - start;
            var strValue = _content.Substring(start, end);
            return strValue;
        }
    }
}
