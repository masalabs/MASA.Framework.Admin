#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Web/MASA.Framework.Admin.Blog/MASA.Framework.Admin.Blog.csproj", "src/Web/MASA.Framework.Admin.Blog/"]
COPY ["src/ApiGateways/Caller/MASA.Framework.Admin.Caller/MASA.Framework.Admin.Caller.csproj", "src/ApiGateways/Caller/MASA.Framework.Admin.Caller/"]
COPY ["src/Contracts/MASA.Framework.Admin.Contracts.Order/MASA.Framework.Admin.Contracts.Order.csproj", "src/Contracts/MASA.Framework.Admin.Contracts.Order/"]
COPY ["src/Contracts/MASA.Framework.Admin.Contracts.Blogs/MASA.Framework.Admin.Contracts.Blogs.csproj", "src/Contracts/MASA.Framework.Admin.Contracts.Blogs/"]
COPY ["src/Services/MASA.Framework.Admin.Service.Blogs/MASA.Framework.Admin.Service.Blogs.csproj", "src/Services/MASA.Framework.Admin.Service.Blogs/"]
RUN dotnet restore "src/Web/MASA.Framework.Admin.Blog/MASA.Framework.Admin.Blog.csproj"
COPY . .
WORKDIR "/src/src/Web/MASA.Framework.Admin.Blog"
RUN dotnet build "MASA.Framework.Admin.Blog.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MASA.Framework.Admin.Blog.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MASA.Framework.Admin.Blog.dll"]