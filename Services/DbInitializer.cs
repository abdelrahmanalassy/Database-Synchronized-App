using System;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace DatabaseSyncApp.Services
{
    public class DbInitializer
    {
        private readonly string _SqliteConnectionString;

        public DbInitializer(IConfiguration config)
        {
            _SqliteConnectionString = config.GetConnectionString("Sqlite");
        }

        public void InitializeSqlite()
        {
            using var connection = new SqliteConnection(_SqliteConnectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"
            CREATE TABLE IF NOT EXISTS Users (
                Id INTEGER PRIMARY KEY,
                Name TEXT,
                Email TEXT,
                UpdatedAt TEXT
                );";
            command.ExecuteNonQuery();

            Console.WriteLine("The Users Table has been created successfully (If it didn't exist).");
        }
    }
}