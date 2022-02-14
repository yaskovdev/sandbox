# Steps To Reproduce (On Windows)

1. Make sure the `OutputPath` from the `ProtobufWithPipeChild.Program` class points to an existing folder.
2. Clean and build the project:

```powershell
Get-ChildItem . -include bin,obj,x64,Debug -Recurse | ForEach-Object ($_) { Remove-Item $_.FullName -Force -Recurse }
nuget restore .
msbuild . /nr:false /p:Configuration=Release
```

3. Run the `ProtobufWithPipeParent.exe`:

```powershell
.\ProtobufWithPipeParent\bin\Release\net6.0\ProtobufWithPipeParent.exe
```

4. Run `kill $PidPrintedByPreviousCommand` in another terminal.
5. Check the `ProtobufWithPipeChild.StackTrace.txt` file inside the `OutputPath` folder.

Expected: the file does not exist. Actual: the file exists and contains:

```
System.NullReferenceException: Object reference not set to an instance of an object.
   at ProtoBuf.Serializer.DeserializeWithLengthPrefix[T](Stream source, PrefixStyle style, Int32 fieldNumber) in /_/src/protobuf-net/Serializer.cs:line 226
   at ProtoBuf.Serializer.DeserializeWithLengthPrefix[T](Stream source, PrefixStyle style) in /_/src/protobuf-net/Serializer.cs:line 212
   at ProtobufWithPipeChild.Program.Main(String[] args) in C:\dev\git_home\sandbox\ProtobufWithPipeIssue\ProtobufWithPipeChild\Program.cs:line 16
```

This means that `Serializer.DeserializeWithLengthPrefix<int>()` threw the `NullReferenceException`.
