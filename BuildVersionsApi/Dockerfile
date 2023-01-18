#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
RUN apt-get update -y
RUN apt-get install -y iptables iputils-ping dnsutils
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["BuildVersionsApi/BuildVersionsApi.csproj", "BuildVersionsApi/"]
COPY ["BuildVersionsApi.Contract/BuildVersionsApi.Contract.csproj", "BuildVersionsApi.Contract/"]
COPY ["BuildVersionsApi.Data/BuildVersionsApi.Data.csproj", "BuildVersionsApi.Data/"]
COPY ["BuildVersionsApi.Mediator/BuildVersionsApi.Mediator.csproj", "BuildVersionsApi.Mediator/"]
COPY ["BuildVersionsApi.Model/BuildVersionsApi.Model.csproj", "BuildVersionsApi.Model/"]
COPY ["Containers.Common.Types/Containers.Common.Types.csproj", "Containers.Common.Types/"]
COPY ["Containers.Common.HealthCheck/Containers.Common.HealthCheck.csproj", "Containers.Common.HealthCheck/"]
RUN dotnet restore "BuildVersionsApi/BuildVersionsApi.csproj"
COPY . .
WORKDIR "/src/BuildVersionsApi"
RUN dotnet build "BuildVersionsApi.csproj" -c Release -o /app/build

FROM build AS publish
ARG Version
ARG Description
RUN dotnet publish "BuildVersionsApi.csproj" -p:Version="$Version" -p:Description="$Description" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BuildVersionsApi.dll"]