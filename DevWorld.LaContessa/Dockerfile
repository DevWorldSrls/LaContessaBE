#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /DevWorld.LaContessa.API
COPY ["DevWorld.LaContessa.API/DevWorld.LaContessa.API.csproj", "DevWorld.LaContessa.API/"]
COPY ["DevWorld.LaContessa.Command/DevWorld.LaContessa.Command.csproj", "DevWorld.LaContessa.Command/"]
COPY ["DevWorld.LaContessa.Command.Abstractions/DevWorld.LaContessa.Command.Abstractions.csproj", "DevWorld.LaContessa.Command.Abstractions/"]
COPY ["DevWorld.LaContessa.Persistance/DevWorld.LaContessa.Persistance.csproj", "DevWorld.LaContessa.Persistance/"]
COPY ["DevWorld.LaContessa.Domain/DevWorld.LaContessa.Domain.csproj", "DevWorld.LaContessa.Domain/"]
COPY ["DevWorld.LaContessa.Persistance.Migrations/DevWorld.LaContessa.Persistance.Migrations.csproj", "DevWorld.LaContessa.Persistance.Migrations/"]
COPY ["DevWorld.LaContessa.Query/DevWorld.LaContessa.Query.csproj", "DevWorld.LaContessa.Query/"]
COPY ["DevWorld.LaContessa.Query.Abstractions/DevWorld.LaContessa.Query.Abstractions.csproj", "DevWorld.LaContessa.Query.Abstractions/"]
RUN dotnet restore "DevWorld.LaContessa.API/DevWorld.LaContessa.API.csproj"
COPY . .
WORKDIR "/DevWorld.LaContessa.API"
RUN dotnet build "DevWorld.LaContessa.API/DevWorld.LaContessa.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DevWorld.LaContessa.API/DevWorld.LaContessa.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DevWorld.LaContessa.API.dll"]