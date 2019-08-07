FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["FastestRouteCalculator/FastestRouteCalculator.csproj", "FastestRouteCalculator/"]
RUN dotnet restore "FastestRouteCalculator/FastestRouteCalculator.csproj"
COPY . .
WORKDIR "/src/FastestRouteCalculator"
RUN dotnet build "FastestRouteCalculator.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "FastestRouteCalculator.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "FastestRouteCalculator.dll"]