# Database Synchronized App

Database Sync App is a .NET 8 console app that syncs data between SQL Server and SQLite, supporting two-way sync for the Users table with conflict resolution and logging.

### **Setup instructions:**
1. .NET SDK.
2. Created the Users table on both databases (SQL Server & SQLite).
3. Added the `appsettings.json` to Configure the connection strings.
4. Start the application using `dotnet run`.
5. Using SSMS & SQLite Extensions for API testing and editing.


### **Tables to Sync:**
```sql
CREATE TABLE Users ( 
   Id INT PRIMARY KEY, 
   Name NVARCHAR(100), 
   Email NVARCHAR(100), 
   UpdatedAt DATETIME
);
  ```

### **Sync Directions:**
| CLI arguments                   | Description                                                              |
|----------------------------|--------------------------------------------------------------------------|
| `--sql-to-sqlite`                  | To Synchronizing from SQL Server to SQLite                                              |
| `--sync-sqlite-to-sql`      | To Synchronizing from SQLite to SQL Server            |
| `--sync-both`         | To Starting Full Two-Way Synchronization        |
| `--sync-deletion-only-both` | To Starting Deletions Synchronizing Only              |


### **Example Output**

  ```text
PS C:\Users\DELL\OneDrive\Desktop\Database Synchronized App\DatabaseSyncApp> dotnet run -- --sync-sql-to-sqlite
[19:02:58 INF] Starting Sync Application...
The Users Table has been created successfully (If it didn't exist).
info: DatabaseSyncApp.Services.SyncService[0]
      Synchronizing from SQL Server to SQLite...
info: DatabaseSyncApp.Services.SyncService[0]
      Inserted User 1 Into SQLite.
info: DatabaseSyncApp.Services.SyncService[0]
      Inserted User 2 Into SQLite.
info: DatabaseSyncApp.Services.SyncService[0]
      Inserted User 3 Into SQLite.
info: DatabaseSyncApp.Services.SyncService[0]
      SQL Server to SQLite Synchronization Completed
  ```

```text
PS C:\Users\DELL\OneDrive\Desktop\Database Synchronized App\DatabaseSyncApp> dotnet run -- --sync-sql-to-sqlite
[02:01:09 INF] Starting Sync Application...
The Users Table has been created successfully (If it didn't exist).
info: DatabaseSyncApp.Services.SyncService[0]
      Synchronizing from SQL Server to SQLite...
info: DatabaseSyncApp.Services.SyncService[0]
      Inserted User 4 Into SQLite.
info: DatabaseSyncApp.Services.SyncService[0]
      Inserted User 5 Into SQLite.
info: DatabaseSyncApp.Services.SyncService[0]
      Inserted User 6 Into SQLite.
Synchronize from SQL Server to SQLite
info: DatabaseSyncApp.Services.SyncService[0]
      SQL Server to SQLite Synchronization Completed
  ```

```text
PS C:\Users\DELL\OneDrive\Desktop\Database Synchronized App\DatabaseSyncApp> dotnet run -- --sync-sqlite-to-sql
[02:05:08 INF] Starting Sync Application...
The Users Table has been created successfully (If it didn't exist).
info: DatabaseSyncApp.Services.SyncService[0]
      Synchronizing from SQLite to SQL Server...
info: DatabaseSyncApp.Services.SyncService[0]
      Inserted User 12 Into SQL Server.
info: DatabaseSyncApp.Services.SyncService[0]
      Inserted User 13 Into SQL Server.
info: DatabaseSyncApp.Services.SyncService[0]
      Inserted User 14 Into SQL Server.
info: DatabaseSyncApp.Services.SyncService[0]
      Inserted User 15 Into SQL Server.
info: DatabaseSyncApp.Services.SyncService[0]
      Inserted User 16 Into SQL Server.
info: DatabaseSyncApp.Services.SyncService[0]
      Inserted User 17 Into SQL Server.
info: DatabaseSyncApp.Services.SyncService[0]
      Inserted User 18 Into SQL Server.
info: DatabaseSyncApp.Services.SyncService[0]
      Inserted User 19 Into SQL Server.
info: DatabaseSyncApp.Services.SyncService[0]
      Inserted User 20 Into SQL Server.
info: DatabaseSyncApp.Services.SyncService[0]
      Inserted User 21 Into SQL Server.
info: DatabaseSyncApp.Services.SyncService[0]
      SQLite to SQL Server Synchronization Completed
  ```

```text
PS C:\Users\DELL\OneDrive\Desktop\Database Synchronized App\DatabaseSyncApp> dotnet run -- --sync-both
[02:07:02 INF] Starting Sync Application...
The Users Table has been created successfully (If it didn't exist).
info: DatabaseSyncApp.Services.SyncService[0]
      Starting Full Two-Way Synchronization...
info: DatabaseSyncApp.Services.SyncService[0]
      Synchronizing from SQL Server to SQLite...
info: DatabaseSyncApp.Services.SyncService[0]
      Inserted User 7 Into SQLite.
info: DatabaseSyncApp.Services.SyncService[0]
      Inserted User 8 Into SQLite.
info: DatabaseSyncApp.Services.SyncService[0]
      Inserted User 9 Into SQLite.
info: DatabaseSyncApp.Services.SyncService[0]
      Inserted User 10 Into SQLite.
info: DatabaseSyncApp.Services.SyncService[0]
      Inserted User 11 Into SQLite.
info: DatabaseSyncApp.Services.SyncService[0]
      SQL Server to SQLite Synchronization Completed
info: DatabaseSyncApp.Services.SyncService[0]
      Synchronizing from SQLite to SQL Server...
info: DatabaseSyncApp.Services.SyncService[0]
      Inserted User 22 Into SQL Server.
info: DatabaseSyncApp.Services.SyncService[0]
      Inserted User 23 Into SQL Server.
info: DatabaseSyncApp.Services.SyncService[0]
      Inserted User 24 Into SQL Server.
info: DatabaseSyncApp.Services.SyncService[0]
      Inserted User 25 Into SQL Server.
info: DatabaseSyncApp.Services.SyncService[0]
      Inserted User 26 Into SQL Server.
info: DatabaseSyncApp.Services.SyncService[0]
      SQLite to SQL Server Synchronization Completed
info: DatabaseSyncApp.Services.SyncService[0]
      Full Two-Way Synchronization Completed
  ```

```text
PS C:\Users\DELL\OneDrive\Desktop\Database Synchronized App\DatabaseSyncApp> dotnet run -- --sync-deletion-only-both
[02:09:24 INF] Starting Sync Application...
The Users Table has been created successfully (If it didn't exist).
info: DatabaseSyncApp.Services.SyncService[0]
      Starting Deletions Synchronizing Only...
info: DatabaseSyncApp.Services.SyncDeletionService[0]
      Deleted Record 27 From SQLite (not found in SQL Server)
info: DatabaseSyncApp.Services.SyncDeletionService[0]
      Deleted Record 28 From SQLite (not found in SQL Server)
info: DatabaseSyncApp.Services.SyncDeletionService[0]
      Deleted Record 29 From SQLite (not found in SQL Server)
info: DatabaseSyncApp.Services.SyncDeletionService[0]
      Deleted Record 30 From SQLite (not found in SQL Server)
info: DatabaseSyncApp.Services.SyncDeletionService[0]
      Deleted Record 31 From SQLite (not found in SQL Server)
info: DatabaseSyncApp.Services.SyncDeletionService[0]
      Deleted Record 32 From SQL Server (not found in SQLite)
info: DatabaseSyncApp.Services.SyncDeletionService[0]
      Deleted Record 33 From SQL Server (not found in SQLite)
info: DatabaseSyncApp.Services.SyncDeletionService[0]
      Deleted Record 34 From SQL Server (not found in SQLite)
info: DatabaseSyncApp.Services.SyncService[0]
      Deletions Synchronization Only Completed
  ```

### **Notes:**
- On first run, The app will automatically create a `Users` table if it not exist.
- Logs are displayed in the console and saved to `logs/`.

