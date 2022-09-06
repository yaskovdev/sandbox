FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["HttpClient/HttpClient.csproj", "HttpClient/"]
RUN dotnet restore "HttpClient/HttpClient.csproj"
COPY . .
WORKDIR "/src/HttpClient"
RUN dotnet build "HttpClient.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HttpClient.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HttpClient.dll"]
