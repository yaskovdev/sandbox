﻿FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["FillArraySandbox/FillArraySandbox.csproj", "FillArraySandbox/"]
RUN dotnet restore "FillArraySandbox/FillArraySandbox.csproj"
COPY . .
WORKDIR "/src/FillArraySandbox"
RUN dotnet build "FillArraySandbox.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FillArraySandbox.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FillArraySandbox.dll"]
