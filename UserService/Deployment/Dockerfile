# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env

WORKDIR /app

# Copy csproj and restore as distinct layers
COPY ./Core/Core.csproj ./Core/

RUN dotnet restore ./Core

COPY ./Core/ ./Core/

COPY ./UserService/*.csproj ./UserService/

RUN dotnet restore ./UserService

COPY ./UserService/ ./UserService/

RUN dotnet publish ./UserService -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .

EXPOSE 6000

ENV ASPNETCORE_URLS http://+:6000

ENTRYPOINT ["dotnet", "UserService.dll"]
