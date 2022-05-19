# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env

WORKDIR /app

# Copy csproj and restore as distinct layers
COPY ./Core/Core.csproj ./Core/

RUN dotnet restore ./Core

COPY ./Core/ ./Core/

COPY ./ProductService/*.csproj ./ProductService/

RUN dotnet restore ./ProductService

COPY ./ProductService/ ./ProductService/

RUN dotnet publish ./ProductService -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .

EXPOSE 5000

ENV ASPNETCORE_URLS http://+:5000

ENTRYPOINT ["dotnet", "ProductService.dll"]
