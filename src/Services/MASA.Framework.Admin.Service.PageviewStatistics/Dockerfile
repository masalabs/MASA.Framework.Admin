#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["nuget.config", "."]
COPY ["src/Services/MASA.Framework.Admin.Service.PageviewStatistics/MASA.Framework.Admin.Service.PageviewStatistics.csproj", "src/Services/MASA.Framework.Admin.Service.PageviewStatistics/"]
COPY ["src/Contracts/MASA.Framework.Admin.Contracts.PageviewStatistics/MASA.Framework.Admin.Contracts.PageviewStatistics.csproj", "src/Contracts/MASA.Framework.Admin.Contracts.PageviewStatistics/"]
COPY ["src/Contracts/MASA.Framework.Admin.Contracts.Logging/MASA.Framework.Admin.Contracts.Logging.csproj", "src/Contracts/MASA.Framework.Admin.Contracts.Logging/"]
RUN dotnet restore "src/Services/MASA.Framework.Admin.Service.PageviewStatistics/MASA.Framework.Admin.Service.PageviewStatistics.csproj"
COPY . .
WORKDIR "/src/src/Services/MASA.Framework.Admin.Service.PageviewStatistics"
RUN dotnet build "MASA.Framework.Admin.Service.PageviewStatistics.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MASA.Framework.Admin.Service.PageviewStatistics.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MASA.Framework.Admin.Service.PageviewStatistics.dll"]
