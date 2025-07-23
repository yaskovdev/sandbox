# escape=`

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0-windowsservercore-ltsc2019 AS build

# Set working directory
WORKDIR /app

# Copy source code
COPY . .

# Restore NuGet packages
RUN dotnet restore

# Build the application
RUN dotnet publish -c Release WindowsContainerFontsSandbox/WindowsContainerFontsSandbox.csproj -o /app/out

FROM mcr.microsoft.com/windows:1809 AS full

# Copy below stuff
RUN powershell -NoProfile -Command "`
Copy-Item -Path C:\Windows\Fonts -Exclude lucon.ttf -Destination c:\Fonts -Recurse; `
New-Item -ItemType Directory -Force -Path c:\registries; `
reg export 'HKLM\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts' c:\registries\FontsReg.reg ; `
reg export 'HKLM\SOFTWARE\Microsoft\Windows NT\CurrentVersion\FontLink\SystemLink' c:\registries\FontLink.reg ; `
"

# Runtime stage
FROM mcr.microsoft.com/dotnet/framework/runtime:4.7.2-windowsservercore-ltsc2019

COPY --from=full /Fonts/ /Windows/Fonts/

COPY --from=full /registries/ ./registries/

RUN mkdir C:\image

# Set working directory
WORKDIR /app

# Copy built application from build stage
COPY --from=build /app/out .

# Run the application
ENTRYPOINT ["WindowsContainerFontsSandbox.exe"]
