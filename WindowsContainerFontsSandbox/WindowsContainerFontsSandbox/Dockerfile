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

# Runtime stage
FROM mcr.microsoft.com/dotnet/framework/runtime:4.7.2-windowsservercore-ltsc2019

RUN mkdir C:\image

# Set working directory
WORKDIR /app

# Copy built application from build stage
COPY --from=build /app/out .

# Run the application
ENTRYPOINT ["WindowsContainerFontsSandbox.exe"]
