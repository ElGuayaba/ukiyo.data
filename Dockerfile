FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
WORKDIR /app
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS http://*:5100
EXPOSE 5100

FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS builder
ARG Configuration=Release
WORKDIR /src
COPY *.sln ./

COPY Ukiyo.Api.WebApi/*.csproj Ukiyo.Api.WebApi/
COPY Ukiyo.Application.Contract/*.csproj Ukiyo.Application.Contract/
COPY Ukiyo.Application.Implementation/*.csproj Ukiyo.Application.Implementation/
COPY Ukiyo.Common/*.csproj Ukiyo.Common/
COPY Ukiyo.Infrastructure.Contract/*.csproj Ukiyo.Infrastructure.Contract/
COPY Ukiyo.Infrastructure.Implementation/*.csproj Ukiyo.Infrastructure.Implementation/

RUN dotnet restore
COPY . .
WORKDIR /src/Ukiyo
RUN dotnet build -c $Configuration -o /app ../Ukiyo.Api.sln 

FROM builder as publish
ARG Configuration=Release
RUN dotnet publish -c $Configuration -o /app ../Ukiyo.Api.sln

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS runtime
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS http://*:5100
EXPOSE 5100
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Ukiyo.Api.WebApi.dll"]
