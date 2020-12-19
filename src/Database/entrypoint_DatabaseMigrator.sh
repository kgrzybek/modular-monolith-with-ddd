# Wait 30 seconds after SQL Server is running for database creation.
sleep 30;

echo $ASPNETCORE_MyMeetings_IntegrationTests_ConnectionString

dotnet DatabaseMigrator.dll $ASPNETCORE_MyMeetings_IntegrationTests_ConnectionString "/migrations"