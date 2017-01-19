1. cd workspace/aep/taskTracker
2. export taskTracker_Db_Path="/Users/csmalley/workspace/aep/taskTracker/taskTrackerDb.db"
3. dotnet ef database update 
4. dotnet run
5. If you have to delete migrations, enter the following: dotnet ef migrations add initialCreate