# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /src

COPY backend/ToDo.Api/ToDo.Api.csproj backend/ToDo.Api/
RUN dotnet restore backend/To-DoApp/ToDo.Api

COPY . .

WORKDIR /src/backend/ToDo.Api

RUN dotnet publish -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0

WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:10000

EXPOSE 10000

ENTRYPOINT ["dotnet", "ToDo.Api.dll"]