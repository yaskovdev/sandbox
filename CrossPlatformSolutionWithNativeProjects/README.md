# Cross Platform Solution with Native Projects

## Building in macOS

Install .NET SDK and CMake if not already installed:

```bash
brew install --cask dotnet-sdk
brew install cmake
```

Then, build the native library and the .NET project:

```bash
rm -rf ./NativeLibrary/build
mkdir ./NativeLibrary/build
cd ./NativeLibrary/build
cmake ..
make

cd ../..
dotnet build CrossPlatformSolutionWithNativeProjects/CrossPlatformSolutionWithNativeProjects.csproj
dotnet run --project CrossPlatformSolutionWithNativeProjects/CrossPlatformSolutionWithNativeProjects.csproj
```
