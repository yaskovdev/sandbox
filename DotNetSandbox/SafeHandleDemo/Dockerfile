FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SafeHandleDemo/SafeHandleDemo.csproj", "SafeHandleDemo/"]
RUN dotnet restore "SafeHandleDemo/SafeHandleDemo.csproj"
COPY . .
WORKDIR "/src/SafeHandleDemo"
RUN dotnet build "SafeHandleDemo.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SafeHandleDemo.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SafeHandleDemo.dll"]
