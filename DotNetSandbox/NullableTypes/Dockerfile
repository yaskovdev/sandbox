FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["NullableTypes/NullableTypes.csproj", "NullableTypes/"]
RUN dotnet restore "NullableTypes/NullableTypes.csproj"
COPY . .
WORKDIR "/src/NullableTypes"
RUN dotnet build "NullableTypes.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NullableTypes.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NullableTypes.dll"]
