#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["WebApiRestful/WebApiRestful.csproj", "WebApiRestful/"]
COPY ["WebApiRestful.Infrastructure/WebApiRestful.Infrastructure.csproj", "WebApiRestful.Infrastructure/"]
COPY ["WebApiRestful.Data/WebApiRestful.Data.csproj", "WebApiRestful.Data/"]
COPY ["WebApiRestful.Domain/WebApiRestful.Domain.csproj", "WebApiRestful.Domain/"]
COPY ["WebApiRestful.Service/WebApiRestful.Service.csproj", "WebApiRestful.Service/"]
RUN dotnet restore "./WebApiRestful/./WebApiRestful.csproj"
COPY . .
WORKDIR "/src/WebApiRestful"
RUN dotnet build "./WebApiRestful.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./WebApiRestful.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApiRestful.dll"]