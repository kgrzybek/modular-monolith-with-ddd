#!/bin/bash

echo 'Starting sql server..' ;

# Start SQL Server
/opt/mssql/bin/sqlservr &

echo 'SQL server started.';

sleep 30 ;

echo 'Create database..' ;

# Create database
/opt/mssql-tools/bin/sqlcmd -d master -i /scripts/CreateDatabase_Linux.sql -U sa -P Test@12345 ;

echo 'Database created' ;

tail -f /dev/null