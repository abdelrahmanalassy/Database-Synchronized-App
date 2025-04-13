using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using DatabaseSyncApp.Helpers;
using DatabaseSyncApp.Services;

namespace DatabaseSyncApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // To Set a Serilog
            Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("logs/sync.log", rollingInterval: RollingInterval.Day)
            .MinimumLevel.Information()
            .CreateLogger();

            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
            });

            try
            {
                Log.Information("Starting Sync Application...");

                // Connection Settings
                var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

                // Database Initializer
                var dbInitializer = new DbInitializer(config);
                dbInitializer.InitializeSqlite();

                // To Read The Connection Data
                var sqlConnectionString = config.GetConnectionString("SqlServer");
                var sqliteConnectionString = config.GetConnectionString("SQLite");

                // To Create a Database Helper Object
                var databaseHelper = new DatabaseHelper(sqlConnectionString, sqliteConnectionString);

                var syncService = new SyncService(databaseHelper, loggerFactory.CreateLogger<SyncService>(), loggerFactory);

                if (args.Length == 0)
                {
                    Log.Error("No Data Provided. Please use one of the following options:");
                    Log.Error("--sync-sql-to-sqlite");
                    Log.Error("--sync-sqlite-to-sql");
                    Log.Error("--sync-both");
                    Log.Error("--sync-deletion-only-both");
                    return;
                }

                var command = args[0].ToLower();
                switch (command)
                {
                    case "--sync-sql-to-sqlite":
                        await syncService.SyncSqlServerToSqliteAsync();
                        Console.WriteLine("Synchronize from SQL Server to SQLite");
                        break;
                    case "--sync-sqlite-to-sql":
                        await syncService.SyncSqliteToSqlServerAsync();
                        Console.WriteLine("Synchronize from SQLite to SQL Server");
                        break;
                    case "--sync-both":
                        await syncService.SyncBothAsync();
                        Console.WriteLine("Synchronize Both");
                        break;
                    case "--sync-deletion-only-both":
                        await syncService.SyncDeleteOnlyAsync();
                        Console.WriteLine("Synchronize Deletions Only");
                        break;
                    default:
                        Log.Error("Unkown Command: {Command}", command);
                        Log.Error("Please use one of the following options:");
                        Log.Error("--sync-sql-to-sqlite");
                        Log.Error("--sync-sqlite-to-sql");
                        Log.Error("--sync-both");
                        Log.Error("--sync-deletion-only-both");

                        break;
                }
            }

            catch (Exception ex)
            {
                Log.Fatal(ex, "A Fatal Error Occurred During Execution");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}