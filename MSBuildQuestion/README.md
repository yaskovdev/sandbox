# MSBuild Question

```powershell
Get-ChildItem . -include bin,obj,x64 -Recurse | ForEach-Object ($_) { Remove-Item $_.FullName -Force -Recurse }
nuget restore
msbuild /p:Platform=x64 /p:Configuration=Release
.\MSBuildQuestion\bin\x64\Release\net6.0\MSBuildQuestion.exe # Fails with "Unable to load DLL 'NativeLibrary.dll'"

msbuild /p:Platform=x64 /p:Configuration=Release # Let's run MSBuild command again
.\MSBuildQuestion\bin\x64\Release\net6.0\MSBuildQuestion.exe # Works as expected: prints "Hello from NativeLibrary"
```

## Update

Stack Overflow community [helped figure out the issue](https://stackoverflow.com/questions/77672978/msbuild-copytooutputdirectory-does-not-copy-a-native-dll-to-the-output).

`<ProjectReference Include="..\NativeLibrary\NativeLibrary.vcxproj" />` should be deleted from `MSBuildQuestion.csproj`.

Then in `MSBuildQuestion.sln`

```
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "MSBuildQuestion", "MSBuildQuestion\MSBuildQuestion.csproj", "{05152BCD-07D1-46E4-91FF-A1203FAEE661}"
EndProject
```

should be replaced with

```
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "MSBuildQuestion", "MSBuildQuestion\MSBuildQuestion.csproj", "{05152BCD-07D1-46E4-91FF-A1203FAEE661}"
    ProjectSection(ProjectDependencies) = postProject
        {0266CB48-3B98-4432-AB6B-ECBDAC445A42} = {0266CB48-3B98-4432-AB6B-ECBDAC445A42}
    EndProjectSection
EndProject
```
