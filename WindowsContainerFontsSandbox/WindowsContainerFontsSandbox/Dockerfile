# escape=`

FROM mcr.microsoft.com/dotnet/sdk:8.0-windowsservercore-ltsc2019 AS build

WORKDIR /app

COPY . .

RUN dotnet restore

RUN dotnet publish -c Release WindowsContainerFontsSandbox/WindowsContainerFontsSandbox.csproj -o /app/out

# Comment out this line
FROM mcr.microsoft.com/windows:1809 AS fonts

# Comment out this line
RUN powershell -NoProfile -Command "Copy-Item -Path C:\Windows\Fonts -Exclude lucon.ttf -Destination c:\Fonts -Recurse"

FROM mcr.microsoft.com/dotnet/framework/runtime:4.7.2-windowsservercore-ltsc2019

# Comment out this line
COPY --from=fonts /Fonts/ /Windows/Fonts/

RUN mkdir C:\image

WORKDIR /app

COPY --from=build /app/out .

ENTRYPOINT ["WindowsContainerFontsSandbox.exe"]
