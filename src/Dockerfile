#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["API/CompanyName.MyMeetings.API/CompanyName.MyMeetings.API.csproj", "API/CompanyName.MyMeetings.API/"]
COPY ["Modules/UserAccess/Application/CompanyName.MyMeetings.Modules.UserAccess.Application.csproj", "Modules/UserAccess/Application/"]
COPY ["Modules/UserAccess/IntegrationEvents/CompanyName.MyMeetings.Modules.UserAccess.IntegrationEvents.csproj", "Modules/UserAccess/IntegrationEvents/"]
COPY ["BuildingBlocks/Infrastructure/CompanyName.MyMeetings.BuildingBlocks.Infrastructure.csproj", "BuildingBlocks/Infrastructure/"]
COPY ["BuildingBlocks/Domain/CompanyName.MyMeetings.BuildingBlocks.Domain.csproj", "BuildingBlocks/Domain/"]
COPY ["BuildingBlocks/Application/CompanyName.MyMeetings.BuildingBlocks.Application.csproj", "BuildingBlocks/Application/"]
COPY ["Modules/UserAccess/Domain/CompanyName.MyMeetings.Modules.UserAccess.Domain.csproj", "Modules/UserAccess/Domain/"]
COPY ["Modules/Meetings/IntegrationEvents/CompanyName.MyMeetings.Modules.Meetings.IntegrationEvents.csproj", "Modules/Meetings/IntegrationEvents/"]
COPY ["Modules/Meetings/Application/CompanyName.MyMeetings.Modules.Meetings.Application.csproj", "Modules/Meetings/Application/"]
COPY ["Modules/Meetings/Domain/CompanyName.MyMeetings.Modules.Meetings.Domain.csproj", "Modules/Meetings/Domain/"]
COPY ["Modules/Administration/IntegrationEvents/CompanyName.MyMeetings.Modules.Administration.IntegrationEvents.csproj", "Modules/Administration/IntegrationEvents/"]
COPY ["Modules/Payments/IntegrationEvents/CompanyName.MyMeetings.Modules.Payments.IntegrationEvents.csproj", "Modules/Payments/IntegrationEvents/"]
COPY ["Modules/UserAccess/Infrastructure/CompanyName.MyMeetings.Modules.UserAccess.Infrastructure.csproj", "Modules/UserAccess/Infrastructure/"]
COPY ["Modules/Payments/Application/CompanyName.MyMeetings.Modules.Payments.Application.csproj", "Modules/Payments/Application/"]
COPY ["Modules/Payments/Domain/CompanyName.MyMeetings.Modules.Payments.Domain.csproj", "Modules/Payments/Domain/"]
COPY ["Modules/Meetings/Infrastructure/CompanyName.MyMeetings.Modules.Meetings.Infrastructure.csproj", "Modules/Meetings/Infrastructure/"]
COPY ["Modules/Payments/Infrastructure/CompanyName.MyMeetings.Modules.Payments.Infrastructure.csproj", "Modules/Payments/Infrastructure/"]
COPY ["Modules/Administration/Application/CompanyName.MyMeetings.Modules.Administration.Application.csproj", "Modules/Administration/Application/"]
COPY ["Modules/Administration/Domain/CompanyName.MyMeetings.Modules.Administration.Domain.csproj", "Modules/Administration/Domain/"]
COPY ["Modules/Administration/Infrastructure/CompanyName.MyMeetings.Modules.Administration.Infrastructure.csproj", "Modules/Administration/Infrastructure/"]
COPY ["Directory.Packages.props", "Directory.Packages.props"]
COPY ["Directory.Build.props", "Directory.Build.props"]
COPY ["Directory.Build.targets", "Directory.Build.targets"]
RUN dotnet restore "API/CompanyName.MyMeetings.API/CompanyName.MyMeetings.API.csproj"
COPY . .
WORKDIR "/src/API/CompanyName.MyMeetings.API"
RUN dotnet build "CompanyName.MyMeetings.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CompanyName.MyMeetings.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ADD entrypoint.sh /

ENTRYPOINT ["/bin/bash", "/entrypoint.sh"]