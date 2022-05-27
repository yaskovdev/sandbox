FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MediaApp.Bootstrap/MediaApp.Bootstrap.csproj", "MediaApp.Bootstrap/"]
RUN dotnet restore "MediaApp.Bootstrap/MediaApp.Bootstrap.csproj"
COPY . .
WORKDIR "/src/MediaApp.Bootstrap"
RUN dotnet build "MediaApp.Bootstrap.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MediaApp.Bootstrap.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MediaApp.Bootstrap.dll"]
