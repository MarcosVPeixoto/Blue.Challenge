FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY . .
RUN dotnet publish Blue.Challenge.sln -c release -o /build
RUN ls -l /build

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /build ./
ENTRYPOINT ["dotnet", "Blue.Challenge.App.dll"]