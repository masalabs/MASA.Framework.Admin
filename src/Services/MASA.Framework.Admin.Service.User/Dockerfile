#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["nuget.config", "."]
COPY ["src/Services/MASA.Framework.Admin.Service.User/MASA.Framework.Admin.Service.User.csproj", "src/Services/MASA.Framework.Admin.Service.User/"]
COPY ["src/Contracts/MASA.Framework.Admin.Contracts.User/MASA.Framework.Admin.Contracts.User.csproj", "src/Contracts/MASA.Framework.Admin.Contracts.User/"]
COPY ["src/Contracts/MASA.Framework.Admin.Contracts.Base/MASA.Framework.Admin.Contracts.Base.csproj", "src/Contracts/MASA.Framework.Admin.Contracts.Base/"]
RUN dotnet restore "src/Services/MASA.Framework.Admin.Service.User/MASA.Framework.Admin.Service.User.csproj"
COPY . .
WORKDIR "/src/src/Services/MASA.Framework.Admin.Service.User"
RUN dotnet build "MASA.Framework.Admin.Service.User.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MASA.Framework.Admin.Service.User.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MASA.Framework.Admin.Service.User.dll"]