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
        public string SqliteConnectionString { get; set; }

        public DatabaseHelper(string sqlServerConnection, string sqliteConnection)
        {
            SqlServerConnectionString = sqlServerConnection;
            SqliteConnectionString = sqliteConnection;
        }

        // To Get al Users from SQL Server
        public async Task<IEnumerable<User>> GetAllUsersFromSqlServer()
        {
            using var connection = new SqlConnection(SqlServerConnectionString);
            return await connection.QueryAsync<User>("SELECT * FROM Users");
        }

        // To Get al Users from SQLite
        public async Task<IEnumerable<User>> GetAllUsersFromSqlite()
        {
            using var connection = new SqliteConnection(SqliteConnectionString);
            return await connection.QueryAsync<User>("SELECT * FROM Users");
        }

        // To Insert Record into Table 'SQLite' 
        public async Task InsertUserIntoSqlite(User user)
        {
            using var connection = new SqliteConnection(SqliteConnectionString);
            await connection.ExecuteAsync("INSERT INTO Users (Id, Name, Email, UpdatedAt) VALUES (@Id, @Name, @Email, @UpdatedAt)", user);
        }

        // To Update Record into Table 'SQLite' 
        public async Task UpdateUserInSqlite(User user)
        {
            using var connection = new SqliteConnection(SqliteConnectionString);
            await connection.ExecuteAsync("UPDATE Users SET name = @Name, Email = @Email, UpdatedAt = @UpdatedAt WHERE Id = @Id", user);
        }

        // To Insert Record into Table 'SQL Server' 
        public async Task InsertUserIntoSqlServer(User user)
        {
            using var connection = new SqlConnection(SqlServerConnectionString);
            await connection.ExecuteAsync("INSERT INTO Users (Id, Name, Email, UpdatedAt) VALUES (@Id, @Name, @Email, @UpdatedAt)", user);
        }

        // To Update Record into Table 'SQL Server' 
        public async Task UpdateUserInSqlServer(User user)
        {
            using var connection = new SqlConnection(SqlServerConnectionString);
            await connection.ExecuteAsync("UPDATE Users SET name = @Name, Email = @Email, UpdatedAt = @UpdatedAt WHERE Id = @Id", user);
        }

        // To Delete User from SQLite
        public async Task DeleteUserFromSqlite(int id)
        {
            using var connection = new SqliteConnection(SqliteConnectionString);
            await connection.ExecuteAsync("DELETE FROM Users WHERE Id = @Id", new { Id = id });
        }

        // To Delete User from SQL Server
        public async Task DeleteUserFromSqlServer(int id)
        {
            using var connection = new SqlConnection(SqlServerConnectionString);
            await connection.ExecuteAsync("DELETE FROM Users WHERE Id = @Id", new { Id = id });
        }

        // To Get all Ids from SQL Server
        public async Task<List<int>> GetAllIdsFromSqlServer()
        {
            using var connection = new SqlConnection(SqlServerConnectionString);
            var ids = await connection.QueryAsync<int>("SELECT Id FROM Users");
            return ids.ToList();
        }

        // To Get all Ids from SQLite
        public async Task<List<int>> GetAllIdsFromSqlite()
        {
            using var connection = new SqliteConnection(SqliteConnectionString);
            var ids = await connection.QueryAsync<int>("SELECT Id FROM Users");
            return ids.ToList();
        }
    }
}