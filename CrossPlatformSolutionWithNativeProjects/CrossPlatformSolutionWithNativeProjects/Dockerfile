FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["HeapCorruptionSandbox/HeapCorruptionSandbox.csproj", "HeapCorruptionSandbox/"]
RUN dotnet restore "HeapCorruptionSandbox/HeapCorruptionSandbox.csproj"
COPY . .
WORKDIR "/src/HeapCorruptionSandbox"
RUN dotnet build "HeapCorruptionSandbox.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HeapCorruptionSandbox.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HeapCorruptionSandbox.dll"]
