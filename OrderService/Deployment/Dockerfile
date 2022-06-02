# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env

WORKDIR /app

# Copy csproj and restore as distinct layers
COPY ./Core/Core.csproj ./Core/

RUN dotnet restore ./Core

COPY ./Core/ ./Core/

COPY ./OrderService/*.csproj ./OrderService/

RUN dotnet restore ./OrderService

COPY ./OrderService/ ./OrderService/

RUN dotnet publish ./OrderService -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .

EXPOSE 5030

ENV ASPNETCORE_URLS http://+:5030
ENV AWS_ACCOUNT_ID aws_account_id

ENTRYPOINT ["dotnet", "OrderService.dll"]
