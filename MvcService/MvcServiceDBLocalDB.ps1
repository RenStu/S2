sqllocaldb create MSSQLLocalDB
sqllocaldb start MSSQLLocalDB
sqllocaldb share mssqllocaldb MvcServiceLocalDB
sqllocaldb stop MSSQLLocalDB
sqllocaldb start MSSQLLocalDB

dotnet ef database update

& "c:\Program Files\Microsoft SQL Server\110\Tools\Binn\SQLCMD.EXE" -S "(localdb)\.\MvcServiceLocalDB" -d "MvcServiceDB" -Q "create login [nt authority\network service] FROM windows with DEFAULT_DATABASE=MvcServiceDB;use MvcServiceDB;exec sp_addrolemember 'db_owner', 'nt authority\network service';"