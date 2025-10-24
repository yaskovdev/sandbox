# escape=`

FROM mcr.microsoft.com/dotnet/sdk:8.0-windowsservercore-ltsc2019 AS build

WORKDIR /app

COPY . .

RUN dotnet restore

RUN dotnet publish -c Release WindowsContainerFontsSandbox/WindowsContainerFontsSandbox.csproj -o /app/out

# Comment out this line
FROM mcr.microsoft.com/windows/server:ltsc2022 AS fonts

# Comment out this line
RUN powershell -NoProfile -Command "`
Copy-Item -Path C:\Windows\Fonts -Exclude lucon.ttf -Destination c:\Fonts -Recurse; `
New-Item -ItemType Directory -Force -Path c:\registries; `
reg export 'HKLM\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts' c:\registries\FontsReg.reg ; `
reg export 'HKLM\SOFTWARE\Microsoft\Windows NT\CurrentVersion\FontLink\SystemLink' c:\registries\FontLink.reg ; `
"

FROM mcr.microsoft.com/dotnet/framework/runtime:4.7.2-windowsservercore-ltsc2019

# Comment out this line

RUN mkdir C:\image

COPY --from=fonts /Fonts/ /Windows/Fonts/
COPY --from=fonts /registries/ ./registries/

RUN powershell -NoProfile -Command "`
reg import C:\registries\FontsReg.reg; `
reg import C:\registries\FontLink.reg; `
"

WORKDIR /app

COPY --from=build /app/out .

ENTRYPOINT ["WindowsContainerFontsSandbox.exe"]
