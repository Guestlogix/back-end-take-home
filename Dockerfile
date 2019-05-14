FROM microsoft/dotnet:2.2-sdk AS build-env

WORKDIR /app
COPY ./src ./src

RUN dotnet publish ./src/AirTrip.Main/ --output /app/build

# Build runtime image
FROM microsoft/dotnet:2.2-runtime

WORKDIR /app

COPY --from=build-env /app/build ./

ENTRYPOINT ["dotnet", "AirTrip.Main.dll"]