Get-ChildItem . -include bin,obj -Recurse | ForEach-Object ($_) { Remove-Item $_.FullName -Force -Recurse }
if (Test-Path .\BenchmarkDotNet.Artifacts)
{
    Remove-Item .\BenchmarkDotNet.Artifacts -Recurse -Force
}

nuget restore
msbuild /p:Configuration=Release /p:Platform="Any CPU"
.\BenchmarkDotNetBug\bin\Release\net6.0\BenchmarkDotNetBug.exe
