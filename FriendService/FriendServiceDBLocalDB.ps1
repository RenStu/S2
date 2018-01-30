sqllocaldb create MSSQLLocalDB1
sqllocaldb start MSSQLLocalDB1
sqllocaldb share mssqllocaldb1 FriendServiceLocalDB
sqllocaldb stop MSSQLLocalDB1
sqllocaldb start MSSQLLocalDB1

dotnet ef database update

& "c:\Program Files\Microsoft SQL Server\110\Tools\Binn\SQLCMD.EXE" -S "(localdb)\.\FriendServiceLocalDB" -d "FriendServiceDB" -Q "create login [nt authority\network service] FROM windows with DEFAULT_DATABASE=FriendServiceDB;use FriendServiceDB;exec sp_addrolemember 'db_owner', 'nt authority\network service';"