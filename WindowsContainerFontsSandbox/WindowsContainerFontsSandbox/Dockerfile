# escape=`

FROM mcr.microsoft.com/dotnet/sdk:8.0-windowsservercore-ltsc2019 AS build

WORKDIR /app

COPY . .

RUN dotnet restore

RUN dotnet publish -c Release WindowsContainerFontsSandbox/WindowsContainerFontsSandbox.csproj -o /app/out

FROM mcr.microsoft.com/windows/server:ltsc2022

RUN mkdir C:\image

WORKDIR /app

COPY --from=build /app/out .

ENTRYPOINT ["WindowsContainerFontsSandbox.exe"]
