#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/Masa.Framework.Admin.Service/Masa.Framework.Admin.Service.Order.csproj", "src/Services/Masa.Framework.Admin.Service/"]
RUN dotnet restore "src/Services/Masa.Framework.Admin.Service/Masa.Framework.Admin.Service.Order.csproj"
COPY . .
WORKDIR "/src/src/Services/Masa.Framework.Admin.Service"
RUN dotnet build "Masa.Framework.Admin.Service.Order.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Masa.Framework.Admin.Service.Order.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Masa.Framework.Admin.Service.Order.dll"]