FROM microsoft/dotnet:2.2-sdk AS build-env

WORKDIR /build
COPY . /build

#Restore
RUN dotnet restore ./airtrip.sln

RUN dotnet publish ./src/AirTrip.Main/ -o /build/publish --configuration Release

# Build runtime image
FROM microsoft/dotnet:2.2-runtime
WORKDIR /app
COPY --from=build-env /build/publish/ .

ENTRYPOINT ["dotnet", "AirTrip.Main.dll"]