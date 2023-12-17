# MSBuild Question

```powershell
Get-ChildItem . -include bin,obj,x64 -Recurse | ForEach-Object ($_) { Remove-Item $_.FullName -Force -Recurse }
nuget restore
msbuild /p:Platform=x64 /p:Configuration=Release
.\MSBuildQuestion2\bin\x64\Release\net6.0\MSBuildQuestion2.exe # Fails with "Unable to load DLL 'NativeLibrary.dll'"

msbuild /p:Platform=x64 /p:Configuration=Release # Let's run MSBuild command again
.\MSBuildQuestion2\bin\x64\Release\net6.0\MSBuildQuestion2.exe # Works as expected: prints "Hello from NativeLibrary"
```
