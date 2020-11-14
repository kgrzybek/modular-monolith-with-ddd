@ECHO OFF
SETLOCAL
SET CONTAINER_ID=
FOR /f %%i IN ('docker ps -q -f name^=myMeetings-integration-db') DO SET CONTAINER_ID=%%i

IF "%CONTAINER_ID%"=="" (
    ECHO "not found"
) ELSE (
	docker rm --force myMeetings-integration-db
)

docker run --rm --name myMeetings-integration-db -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=61cD4gE6!" -e "MSSQL_PID=Express" -p 1439:1433 -d mcr.microsoft.com/mssql/server:2017-latest-ubuntu
TIMEOUT 30
docker cp ./src/Database/CompanyName.MyMeetings.Database/Scripts/CreateDatabase_Linux.sql myMeetings-integration-db:/
docker exec -i myMeetings-integration-db sh -c "/opt/mssql-tools/bin/sqlcmd -d master -i /CreateDatabase_Linux.sql -U sa -P 61cD4gE6!"
dotnet build src/ --configuration Release --no-restore
SET ASPNETCORE_MyMeetings_IntegrationTests_ConnectionString=Server=localhost,1439;Database=MyMeetings;User=sa;Password=61cD4gE6!
dotnet "src/Database/DatabaseMigrator/bin/Release/netcoreapp3.1/DatabaseMigrator.dll" %ASPNETCORE_MyMeetings_IntegrationTests_ConnectionString% "src/Database/CompanyName.MyMeetings.Database/Scripts/Migrations"
dotnet test --configuration Release --no-build --verbosity normal src/Modules/Administration/Tests/IntegrationTests/CompanyName.MyMeetings.Modules.Administration.IntegrationTests.csproj
dotnet test --configuration Release --no-build --verbosity normal src/Modules/Payments/Tests/IntegrationTests/CompanyName.MyMeetings.Modules.Payments.IntegrationTests.csproj
dotnet test --configuration Release --no-build --verbosity normal src/Modules/UserAccess/Tests/IntegrationTests/CompanyNames.MyMeetings.Modules.UserAccess.IntegrationTests.csproj
dotnet test --configuration Release --no-build --verbosity normal src/Modules/Meetings/Tests/IntegrationTests/CompanyName.MyMeetings.Modules.Meetings.IntegrationTests.csproj
dotnet test --configuration Release --no-build --verbosity normal src/Tests/IntegrationTests/CompanyName.MyMeetings.IntegrationTests.csproj
