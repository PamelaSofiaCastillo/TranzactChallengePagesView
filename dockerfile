FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY . ./
RUN dotnet restore TranzactChallengePagesViews/TranzactChallengePagesViews.sln

# # Copy everything else and build
# COPY ../engine/examples ./
RUN dotnet publish TranzactChallengePagesViews -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build-env /app/TranzactChallengePagesViews/bin/Release/net5.0 .

ENTRYPOINT ["dotnet", "TranzactChallengePagesViews.dll"]