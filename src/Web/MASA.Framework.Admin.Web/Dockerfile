#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Web/MASA.Framework.Admin.Web/MASA.Framework.Admin.Web.csproj", "src/Web/MASA.Framework.Admin.Web/"]
RUN dotnet restore "src/Web/MASA.Framework.Admin.Web/MASA.Framework.Admin.Web.csproj"
COPY . .
WORKDIR "/src/src/Web/MASA.Framework.Admin.Web"
RUN dotnet build "MASA.Framework.Admin.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MASA.Framework.Admin.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
RUN dotnet dev-certs https --clean
RUN dotnet dev-certs https -ep .\MASA.Framework.Admin.Web.pfx -p P@ssw0rd
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MASA.Framework.Admin.Web.dll"]