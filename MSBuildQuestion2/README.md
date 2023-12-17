# MSBuild Question 2

```powershell
Get-ChildItem . -include bin,obj,x64 -Recurse | ForEach-Object ($_) { Remove-Item $_.FullName -Force -Recurse }
nuget restore
msbuild /p:Platform=x64 /p:Configuration=Release
.\MSBuildQuestion2\bin\x64\Release\net6.0\MSBuildQuestion2.exe # Fails with "Unable to load DLL 'NativeLibrary.dll'"

msbuild /p:Platform=x64 /p:Configuration=Release # Let's run MSBuild command again
.\MSBuildQuestion2\bin\x64\Release\net6.0\MSBuildQuestion2.exe # Works as expected: prints "Hello from NativeLibrary"
```

Currently, the project tree is like
this: `MSBuildQuestion2.csproj` -> `NativeLibraryWrapper.csproj` -> `NativeLibrary.vcxproj` (first
via `ProjectReference`, second via `ProjectDependencies` in solution file)

Possible fix is to add `NativeLibrary.dll` to `ProjectDependencies` of the `MSBuildQuestion2.csproj` in the solution
file as well. But that does not seem to be a robust solution, because this means that whoever wants to reference
`NativeLibraryWrapper` via `ProjectReference` (directly _or transitively_) should also not forget to reference
`NativeLibrary.dll` via `ProjectDependencies`.
