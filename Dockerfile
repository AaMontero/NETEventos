FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY EventosServicio.csproj .
RUN dotnet restore
COPY . .
RUN dotnet build "EventosServicio.csproj" -c Release -o /app/build
RUN dotnet publish "EventosServicio.csproj" -c Release -o /app
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "EventosServicio.dll"]
