#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["DevWorld.LaContessa.SetupJob/DevWorld.LaContessa.SetupJob.csproj", "DevWorld.LaContessa.SetupJob/"]
COPY ["DevWorld.LaContessa.Persistance.Migrations/DevWorld.LaContessa.Persistance.Migrations.csproj", "DevWorld.LaContessa.Persistance.Migrations/"]
COPY ["DevWorld.LaContessa.Persistance/DevWorld.LaContessa.Persistance.csproj", "DevWorld.LaContessa.Persistance/"]
COPY ["DevWorld.LaContessa.Domain/DevWorld.LaContessa.Domain.csproj", "DevWorld.LaContessa.Domain/"]
RUN dotnet restore "DevWorld.LaContessa.SetupJob/DevWorld.LaContessa.SetupJob.csproj"
COPY . .
WORKDIR "/src/DevWorld.LaContessa.SetupJob"
RUN dotnet build "DevWorld.LaContessa.SetupJob.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DevWorld.LaContessa.SetupJob.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DevWorld.LaContessa.SetupJob.dll"]