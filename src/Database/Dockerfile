FROM mcr.microsoft.com/mssql/server:2017-latest-ubuntu

COPY ./CompanyName.MyMeetings.Database/Scripts/CreateDatabase_Linux.sql /scripts/

ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=Test@12345
ENV MSSQL_PID=Express
ENV MSSQL_TCP_PORT=1433

ADD entrypoint.sh /
ENTRYPOINT ["/bin/bash", "/entrypoint.sh"]