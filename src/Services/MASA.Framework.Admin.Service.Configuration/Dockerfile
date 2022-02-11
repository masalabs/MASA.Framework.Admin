#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["nuget.config", "."]
COPY ["src/Services/MASA.Framework.Admin.Configuration/MASA.Framework.Admin.Configuration.csproj", "src/Services/MASA.Framework.Admin.Configuration/"]
COPY ["src/Contracts/MASA.Framework.Admin.Contracts.Configuration/MASA.Framework.Admin.Contracts.Configuration.csproj", "src/Contracts/MASA.Framework.Admin.Contracts.Configuration/"]
COPY ["src/Contracts/MASA.Framework.Admin.Contracts.Base/MASA.Framework.Admin.Contracts.Base.csproj", "src/Contracts/MASA.Framework.Admin.Contracts.Base/"]
RUN dotnet restore "src/Services/MASA.Framework.Admin.Configuration/MASA.Framework.Admin.Configuration.csproj"
COPY . .
WORKDIR "/src/src/Services/MASA.Framework.Admin.Configuration"
RUN dotnet build "MASA.Framework.Admin.Configuration.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MASA.Framework.Admin.Configuration.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MASA.Framework.Admin.Configuration.dll"]