#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["nuget.config", "."]
COPY ["src/Services/MASA.Framework.Admin.Service.Authentication/MASA.Framework.Admin.Service.Authentication.csproj", "src/Services/MASA.Framework.Admin.Service.Authentication/"]
COPY ["src/Contracts/MASA.Framework.Admin.Contracts.Authentication/MASA.Framework.Admin.Contracts.Authentication.csproj", "src/Contracts/MASA.Framework.Admin.Contracts.Authentication/"]
COPY ["src/Contracts/MASA.Framework.Admin.Contracts.Base/MASA.Framework.Admin.Contracts.Base.csproj", "src/Contracts/MASA.Framework.Admin.Contracts.Base/"]
RUN dotnet restore "src/Services/MASA.Framework.Admin.Service.Authentication/MASA.Framework.Admin.Service.Authentication.csproj"
COPY . .
WORKDIR "/src/src/Services/MASA.Framework.Admin.Service.Authentication"
RUN dotnet build "MASA.Framework.Admin.Service.Authentication.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MASA.Framework.Admin.Service.Authentication.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MASA.Framework.Admin.Service.Authentication.dll"]