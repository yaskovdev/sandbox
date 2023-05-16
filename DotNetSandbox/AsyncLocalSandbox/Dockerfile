FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AsyncLocalSandbox/AsyncLocalSandbox.csproj", "AsyncLocalSandbox/"]
RUN dotnet restore "AsyncLocalSandbox/AsyncLocalSandbox.csproj"
COPY . .
WORKDIR "/src/AsyncLocalSandbox"
RUN dotnet build "AsyncLocalSandbox.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AsyncLocalSandbox.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AsyncLocalSandbox.dll"]
