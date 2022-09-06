FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DllImportSandbox/DllImportSandbox.csproj", "DllImportSandbox/"]
RUN dotnet restore "DllImportSandbox/DllImportSandbox.csproj"
COPY . .
WORKDIR "/src/DllImportSandbox"
RUN dotnet build "DllImportSandbox.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DllImportSandbox.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DllImportSandbox.dll"]
