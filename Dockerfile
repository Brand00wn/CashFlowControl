FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

COPY src/ /app/src/

RUN dotnet restore /app/src/App/LaunchService/LaunchService.csproj

RUN dotnet restore /app/src/App/AuthenticationService/AuthenticationService.csproj

RUN dotnet restore /app/src/App/ConsolidationService/ConsolidationService.csproj

RUN dotnet publish /app/src/App/LaunchService/LaunchService.csproj -c Release -o /out

RUN dotnet publish /app/src/App/AuthenticationService/AuthenticationService.csproj -c Release -o /out

RUN dotnet publish /app/src/App/ConsolidationService/ConsolidationService.csproj -c Release -o /out