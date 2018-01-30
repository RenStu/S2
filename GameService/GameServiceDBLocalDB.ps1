sqllocaldb create MSSQLLocalDB2
sqllocaldb start MSSQLLocalDB2
sqllocaldb share mssqllocaldb2 GameServiceLocalDB
sqllocaldb stop MSSQLLocalDB2
sqllocaldb start MSSQLLocalDB2

dotnet ef database update

& "c:\Program Files\Microsoft SQL Server\110\Tools\Binn\SQLCMD.EXE" -S "(localdb)\.\GameServiceLocalDB" -d "GameServiceDB" -Q "create login [nt authority\network service] FROM windows with DEFAULT_DATABASE=GameServiceDB;use GameServiceDB;exec sp_addrolemember 'db_owner', 'nt authority\network service';"