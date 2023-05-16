FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["FillArrayBenchmark/FillArrayBenchmark.csproj", "FillArrayBenchmark/"]
RUN dotnet restore "FillArrayBenchmark/FillArrayBenchmark.csproj"
COPY . .
WORKDIR "/src/FillArrayBenchmark"
RUN dotnet build "FillArrayBenchmark.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FillArrayBenchmark.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FillArrayBenchmark.dll"]
