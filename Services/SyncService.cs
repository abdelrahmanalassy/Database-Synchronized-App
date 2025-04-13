using Microsoft.Extensions.Logging;
using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using System.Linq;
using DatabaseSyncApp.Services;
using DatabaseSyncApp.Helpers;



namespace DatabaseSyncApp.Services
{
    public class SyncService
    {
        private readonly DatabaseHelper _databaseHelper;
        private readonly ILogger<SyncService> _logger;
        private readonly SyncDeletionService _deletionService;

        public SyncService(DatabaseHelper databaseHelper, ILogger<SyncService> logger, ILoggerFactory loggerFactory)
        {
            _databaseHelper = databaseHelper;
            _logger = logger;
            _deletionService = new SyncDeletionService(databaseHelper, loggerFactory.CreateLogger<SyncDeletionService>());
        }

        public async Task SyncSqlServerToSqliteAsync()
        {
            _logger.LogInformation("Synchronizing from SQL Server to SQLite...");

            var sqlUsers = await _databaseHelper.GetAllUsersFromSqlServer();
            var sqliteUsers = await _databaseHelper.GetAllUsersFromSqlite();

            foreach (var user in sqlUsers)
            {
                var existing = sqliteUsers.FirstOrDefault(u => u.Id == user.Id);

                if (existing == null)
                {
                    await _databaseHelper.InsertUserIntoSqlite(user);
                    _logger.LogInformation($"Inserted User {user.Id} Into SQLite.");
                }
                else if (user.UpdatedAt > existing.UpdatedAt)
                {
                    await _databaseHelper.UpdateUserInSqlite(user);
                    _logger.LogInformation($"Updated User {user.Id} in SQLite.");
                }
            }
            _logger.LogInformation("SQL Server to SQLite Synchronization Completed");
        }

        public async Task SyncSqliteToSqlServerAsync()
        {
            _logger.LogInformation("Synchronizing from SQLite to SQL Server... ");

            var sqlUsers = await _databaseHelper.GetAllUsersFromSqlServer();
            var sqliteUsers = await _databaseHelper.GetAllUsersFromSqlite();

            foreach (var user in sqliteUsers)
            {
                var existing = sqlUsers.FirstOrDefault(u => u.Id == user.Id);

                if (existing == null)
                {
                    await _databaseHelper.InsertUserIntoSqlServer(user);
                    _logger.LogInformation($"Inserted User {user.Id} Into SQL Server.");
                }
                else if (user.UpdatedAt > existing.UpdatedAt)
                {
                    await _databaseHelper.UpdateUserInSqlServer(user);
                    _logger.LogInformation($"Updated User {user.Id} in SQL Server.");
                }
            }
            _logger.LogInformation("SQLite to SQL Server Synchronization Completed");
        }

        public async Task SyncBothAsync()
        {
            _logger.LogInformation("Starting Full Two-Way Synchronization...");

            await SyncSqlServerToSqliteAsync();
            await SyncSqliteToSqlServerAsync();

            await _deletionService.SyncDeletionFromSqlServerToSqlite();
            await _deletionService.SyncDeletionFromSqliteToSqlServer();

            _logger.LogInformation("Full Two-Way Synchronization Completed");
        }

        public async Task SyncDeleteOnlyAsync()
        {
            _logger.LogInformation("Starting Deletions Synchronizing Only...");

            await _deletionService.SyncDeletionFromSqlServerToSqlite();
            await _deletionService.SyncDeletionFromSqliteToSqlServer();

            _logger.LogInformation("Deletions Synchronization Only Completed");
        }
    }
}