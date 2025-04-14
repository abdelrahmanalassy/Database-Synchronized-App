# Database Synchronized App

Database Sync App is a .NET 8 console app that syncs data between SQL Server and SQLite, supporting two-way sync for the Users table with conflict resolution and logging.

### **Setup instructions:**
1. Created the `Users` table on both databases (SQL Server & SQLite).
2. Added the `appsettings.json` to Configure the connection strings.
3. Start the application using `dotnet run`.
4. Using SSMS & SQLite Extensions for testing and editing.


### **SQL Server Table Schema for Users:**
```sql
CREATE TABLE Users ( 
   Id INT PRIMARY KEY, 
   Name NVARCHAR(100), 
   Email NVARCHAR(100), 
   UpdatedAt DATETIME
);
  ```

### **SQLite Table Schema for Users:**
```sql
CREATE TABLE Users ( 
   Id INTEGER PRIMARY KEY, 
   Name TEXT, 
   Email TEXT, 
   UpdatedAt TEXT
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
[11:11:36 INF] Starting Sync Application...
The Users Table has been created successfully (If it didn't exist).
[11:11:36 INF] Synchronizing from SQL Server to SQLite...
[11:11:37 INF] Inserted User 27 Into SQLite.
[11:11:37 INF] Inserted User 28 Into SQLite.
[11:11:37 INF] Inserted User 29 Into SQLite.
[11:11:37 INF] Inserted User 30 Into SQLite.
[11:11:37 INF] Inserted User 31 Into SQLite.
[11:11:37 INF] Inserted User 32 Into SQLite.
[11:11:37 INF] Inserted User 33 Into SQLite.
[11:11:37 INF] Inserted User 34 Into SQLite.
[11:11:37 INF] Inserted User 35 Into SQLite.
[11:11:37 INF] Inserted User 36 Into SQLite.
[11:11:37 INF] Inserted User 37 Into SQLite.
[11:11:37 INF] Inserted User 45 Into SQLite.
[11:11:37 INF] Inserted User 46 Into SQLite.
[11:11:37 INF] Inserted User 47 Into SQLite.
[11:11:37 INF] Inserted User 48 Into SQLite.
[11:11:37 INF] Inserted User 49 Into SQLite.
[11:11:37 INF] Inserted User 50 Into SQLite.
[11:11:37 INF] Inserted User 51 Into SQLite.
[11:11:37 INF] Inserted User 52 Into SQLite.
[11:11:37 INF] Inserted User 53 Into SQLite.
[11:11:37 INF] Inserted User 54 Into SQLite.
[11:11:37 INF] Inserted User 55 Into SQLite.
[11:11:37 INF] Inserted User 56 Into SQLite.
[11:11:37 INF] Inserted User 57 Into SQLite.
[11:11:37 INF] Inserted User 58 Into SQLite.
[11:11:37 INF] Inserted User 59 Into SQLite.
[11:11:37 INF] SQL Server to SQLite Synchronization Completed
  ```

```text
PS C:\Users\DELL\OneDrive\Desktop\Database Synchronized App\DatabaseSyncApp> dotnet run -- --sync-sqlite-to-sql
[11:12:12 INF] Starting Sync Application...
The Users Table has been created successfully (If it didn't exist).
[11:12:12 INF] Synchronizing from SQLite to SQL Server... 
[11:12:12 INF] Inserted User 60 Into SQL Server.
[11:12:12 INF] Inserted User 61 Into SQL Server.
[11:12:12 INF] Inserted User 62 Into SQL Server.
[11:12:12 INF] Inserted User 63 Into SQL Server.
[11:12:12 INF] Inserted User 64 Into SQL Server.
[11:12:12 INF] Inserted User 65 Into SQL Server.
[11:12:12 INF] SQLite to SQL Server Synchronization Completed
  ```

```text
PS C:\Users\DELL\OneDrive\Desktop\Database Synchronized App\DatabaseSyncApp> dotnet run -- --sync-both
[11:13:43 INF] Starting Sync Application...
The Users Table has been created successfully (If it didn't exist).
[11:13:43 INF] Starting Full Two-Way Synchronization...
[11:13:43 INF] Synchronizing from SQL Server to SQLite...
[11:13:43 INF] Inserted User 66 Into SQLite.
[11:13:43 INF] Inserted User 67 Into SQLite.
[11:13:43 INF] Inserted User 68 Into SQLite.
[11:13:43 INF] SQL Server to SQLite Synchronization Completed
[11:13:43 INF] Synchronizing from SQLite to SQL Server...
[11:13:43 INF] Inserted User 69 Into SQL Server.
[11:13:43 INF] Inserted User 70 Into SQL Server.
[11:13:43 INF] Inserted User 71 Into SQL Server.
[11:13:43 INF] SQLite to SQL Server Synchronization Completed
[11:13:43 INF] Full Two-Way Synchronization Completed
  ```

```text
PS C:\Users\DELL\OneDrive\Desktop\Database Synchronized App\DatabaseSyncApp> dotnet run -- --sync-deletion-only-both
[11:15:01 INF] Starting Sync Application...
The Users Table has been created successfully (If it didn't exist).
[11:15:01 INF] Starting Deletions Synchronizing Only...
[11:15:01 INF] Deleted Record 72 From SQLite (not found in SQL Server)
[11:15:01 INF] Deleted Record 73 From SQLite (not found in SQL Server)
[11:15:01 INF] Deleted Record 74 From SQLite (not found in SQL Server)
[11:15:01 INF] Deleted Record 75 From SQLite (not found in SQL Server)
[11:15:01 INF] Deleted Record 76 From SQLite (not found in SQL Server)
[11:15:01 INF] Deleted Record 77 From SQL Server (not found in SQLite)
[11:15:01 INF] Deleted Record 78 From SQL Server (not found in SQLite)
[11:15:01 INF] Deleted Record 79 From SQL Server (not found in SQLite)
[11:15:01 INF] Deleted Record 80 From SQL Server (not found in SQLite)
[11:15:01 INF] Deletions Synchronization Only Completed
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
- On first run, The app will automatically create a `Users` table if it not exist
- Logs are displayed in the console and saved to `logs/sync.log`

