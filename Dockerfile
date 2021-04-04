
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR "/src"
COPY ["Carsales.StockManagement.Api/Carsales.StockManagement.Api.csproj", "Carsales.StockManagement.Api/"]
COPY ["Carsales.StockManagement.Api.Tests/Carsales.StockManagement.Api.Tests.csproj", "Carsales.StockManagement.Api.Tests/"]
RUN dotnet restore "Carsales.StockManagement.Api/Carsales.StockManagement.Api.csproj"
COPY . .
WORKDIR "/src/Carsales.StockManagement.Api"
RUN dotnet build "Carsales.StockManagement.Api.csproj" -c Release -o /app/build

WORKDIR "/src/Carsales.StockManagement.Api.Tests"
RUN dotnet test "Carsales.StockManagement.Api.Tests.csproj"

WORKDIR "/src/Carsales.StockManagement.Api"
FROM build AS publish
RUN dotnet publish "Carsales.StockManagement.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Carsales.StockManagement.Api.dll"]
