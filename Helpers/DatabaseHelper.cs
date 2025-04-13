using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using DatabaseSyncApp.Models;
using DatabaseSyncApp.Services;
using Dapper;

namespace DatabaseSyncApp.Helpers
{
    public class DatabaseHelper
    {
        public string SqlServerConnectionString { get; set; }
        public string  SqliteConnectionString { get; set; }

        public DatabaseHelper(string sqlServerConnection, string sqliteConnection)
        {
            SqlServerConnectionString = sqlServerConnection;
            SqliteConnectionString = sqliteConnection;
        }

        public async Task<IEnumerable<User>> GetAllUsersFromSqlServer()
        {
            using var connection = new SqlConnection(SqlServerConnectionString);
            return await connection.QueryAsync<User>("SELECT * FROM Users");
        }

        public async Task<IEnumerable<User>> GetAllUsersFromSqlite()
        {
            using var connection = new SqliteConnection(SqliteConnectionString);
            return await connection.QueryAsync<User>("SELECT * FROM Users");
        }

        public async Task InsertUserIntoSqlite(User user)
        {
            using var connection = new SqliteConnection(SqliteConnectionString);
            await connection.ExecuteAsync("INSERT INTO Users (Id, Name, Email, UpdatedAt) VALUES (@Id, @Name, @Email, @UpdatedAt)", user);
        }

        public async Task UpdateUserInSqlite(User user)
        {
            using var connection = new SqliteConnection(SqliteConnectionString);
            await connection.ExecuteAsync("UPDATE Users SET name = @Name, Email = @Email, UpdatedAt = @UpdatedAt WHERE Id = @Id", user);
        }

        public async Task InsertUserIntoSqlServer(User user)
        {
            using var connection = new SqlConnection(SqlServerConnectionString);
            await connection.ExecuteAsync("INSERT INTO Users (Id, Name, Email, UpdatedAt) VALUES (@Id, @Name, @Email, @UpdatedAt)", user);
        }
        public async Task UpdateUserInSqlServer(User user)
        {
            using var connection = new SqlConnection(SqlServerConnectionString);
            await connection.ExecuteAsync("UPDATE Users SET name = @Name, Email = @Email, UpdatedAt = @UpdatedAt WHERE Id = @Id", user);
        }

        public async Task DeleteUserFromSqlite(int id)
        {
            using var connection = new SqliteConnection(SqliteConnectionString);
            await connection.ExecuteAsync("DELETE FROM Users WHERE Id = @Id", new { Id = id });
        }

        public async Task DeleteUserFromSqlServer(int id)
        {
            using var connection = new SqlConnection(SqlServerConnectionString);
            await connection.ExecuteAsync("DELETE FROM Users WHERE Id = @Id", new { Id = id });
        }

        public async Task<List<int>> GetAllIdsFromSqlServer()
        {
            using var connection = new SqlConnection(SqlServerConnectionString);
            var ids = await connection.QueryAsync<int>("SELECT Id FROM Users");
            return ids.ToList();
        }

        public async Task<List<int>> GetAllIdsFromSqlite()
        {
            using var connection = new SqliteConnection(SqliteConnectionString);
            var ids = await connection.QueryAsync<int>("SELECT Id FROM Users");
            return ids.ToList();
        }
    }     
}