FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Record/Record.csproj", "Record/"]
RUN dotnet restore "Record/Record.csproj"
COPY . .
WORKDIR "/src/Record"
RUN dotnet build "Record.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Record.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Record.dll"]
