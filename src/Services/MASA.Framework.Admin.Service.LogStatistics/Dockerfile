#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["nuget.config", "."]
COPY ["src/Services/MASA.Framework.Admin.Service.LogStatistics/MASA.Framework.Admin.Service.LogStatistics.csproj", "src/Services/MASA.Framework.Admin.Service.LogStatistics/"]
COPY ["src/Infrastructure/MASA.Framework.Admin.Infrastructure.Configurations/MASA.Framework.Admin.Infrastructure.Configurations.csproj", "src/Infrastructure/MASA.Framework.Admin.Infrastructure.Configurations/"]
COPY ["sdks/MASA.Framework.Sdks.Authentication/MASA.Framework.Sdks.Authentication.csproj", "sdks/MASA.Framework.Sdks.Authentication/"]
RUN dotnet restore "src/Services/MASA.Framework.Admin.Service.LogStatistics/MASA.Framework.Admin.Service.LogStatistics.csproj"
COPY . .
WORKDIR "/src/src/Services/MASA.Framework.Admin.Service.LogStatistics"
RUN dotnet build "MASA.Framework.Admin.Service.LogStatistics.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MASA.Framework.Admin.Service.LogStatistics.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MASA.Framework.Admin.Service.LogStatistics.dll"]
