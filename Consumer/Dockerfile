FROM mcr.microsoft.com/dotnet/sdk:6.0.100-preview.5-alpine3.13-amd64 AS build-env
WORKDIR /build

# Copy csproj and restore as distinct layers
COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/runtime:6.0.0-preview.5-alpine3.13-amd64
WORKDIR /app
COPY --from=build-env /build/out .
COPY --from=build-env /build/appsettings.json ./appsettings.json
ENTRYPOINT ["dotnet", "redis-csharp.dll"]