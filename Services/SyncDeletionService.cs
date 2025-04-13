using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using DatabaseSyncApp.Models;
using DatabaseSyncApp.Services;
using DatabaseSyncApp.Helpers;


namespace DatabaseSyncApp.Services
{
    public class SyncDeletionService
    {
        private readonly DatabaseHelper _databaseHelper;
        private readonly ILogger<SyncDeletionService> _logger;

        public SyncDeletionService(DatabaseHelper databaseHelper, ILogger<SyncDeletionService> logger)
        {
            _databaseHelper = databaseHelper;
            _logger = logger;
        }

        // To Get All Ids From SQL Server
        public async Task<List<int>> GetAllIdsFromSqlServer()
        {
            var ids = new List<int>();
            using var connection = new SqlConnection(_databaseHelper.SqlServerConnectionString);
            await connection.OpenAsync();

            using var command = new SqlCommand("SELECT Id FROM Users", connection);
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                ids.Add(reader.GetInt32(0));
            }
            return ids;
        }

        // To Get All Ids From SQLite
        public async Task<List<int>> GetAllIdsFromSqlite()
        {
            var ids = new List<int>();
            using var connection = new SqliteConnection(_databaseHelper.SqliteConnectionString);
            await connection.OpenAsync();

            using var command = new SqliteCommand("SELECT Id FROM Users", connection);
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                ids.Add(reader.GetInt32(0));
            }
            return ids;
        }

        // To Delete Records From SQL Server
        public async Task DeleteUserFromSqlServer(int id)
        {
            using var connection = new SqlConnection(_databaseHelper.SqlServerConnectionString);
            await connection.OpenAsync();

            using var command = new SqlCommand("DELETE FROM Users WHERE Id = @Id", connection);
            command.Parameters.AddWithValue("@Id", id);

            await command.ExecuteNonQueryAsync();
        }

        // To Delete Records From SQLite
        public async Task DeleteUserFromSqlite(int id)
        {
            using var connection = new SqliteConnection(_databaseHelper.SqliteConnectionString);
            await connection.OpenAsync();

            using var command = new SqliteCommand("DELETE FROM Users WHERE Id = @Id", connection);
            command.Parameters.AddWithValue("@Id", id);

            await command.ExecuteNonQueryAsync();
        }

        // Delete Records From SQLite If Not Found In SQL Server
        public async Task SyncDeletionFromSqlServerToSqlite()
        {
            var sqlIds = await GetAllIdsFromSqlServer();
            var sqliteIds = await GetAllIdsFromSqlite();

            var idsToDelete = sqliteIds.Except(sqlIds).ToList();

            foreach (var id in idsToDelete)
            {
                await DeleteUserFromSqlite(id);
                _logger.LogInformation($"Deleted Record {id} From SQLite (not found in SQL Server)");
            }
        }
        // Delete Records From SQL Server If Not Found In SQLite
        public async Task SyncDeletionFromSqliteToSqlServer()
        {
            var sqlIds = await GetAllIdsFromSqlServer();
            var sqliteIds = await GetAllIdsFromSqlite();

            var idsToDelete = sqlIds.Except(sqliteIds).ToList();

            foreach (var id in idsToDelete)
            {
                await DeleteUserFromSqlServer(id);
                _logger.LogInformation($"Deleted Record {id} From SQL Server (not found in SQLite)");
            }
        }

    }
}