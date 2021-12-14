FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["AsyncAwait/AsyncAwait.csproj", "AsyncAwait/"]
RUN dotnet restore "AsyncAwait/AsyncAwait.csproj"
COPY . .
WORKDIR "/src/AsyncAwait"
RUN dotnet build "AsyncAwait.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AsyncAwait.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AsyncAwait.dll"]
