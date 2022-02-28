#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Web/Masa.Framework.Admin.Web/Masa.Framework.Admin.Web.csproj", "src/Web/Masa.Framework.Admin.Web/"]
RUN dotnet restore "src/Web/Masa.Framework.Admin.Web/Masa.Framework.Admin.Web.csproj"
COPY . .
WORKDIR "/src/src/Web/Masa.Framework.Admin.Web"
RUN dotnet build "Masa.Framework.Admin.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Masa.Framework.Admin.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Masa.Framework.Admin.Web.dll"]