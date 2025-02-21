# Experiment 1

Try building the project with:

```powershell
dotnet clean
dotnet build EditorConfigSandbox/EditorConfigSandbox.csproj
```

The build with fail with the next error: "error CA1036: RatingInformation should define operator(s) '==, !=, <, <=, >, >=' and Equals since it implements IComparable".

Now replace `error` with `warning` in `.editorconfig` and try building again with the same command. The build will succeed.

Outcome: the C# compiler is taking the `.editorconfig` into consideration.

# Experiment 2

1. Add `<TreatWarningsAsErrors>true</TreatWarningsAsErrors>` to `EditorConfigSandbox.csproj`.
2. Set `dotnet_diagnostic.CA1036.severity` to `warning` in `.editorconfig`.
3. Try building from the command line with the above command.

The build should fail because compiler respects both `<TreatWarningsAsErrors>true</TreatWarningsAsErrors>` and `.editorconfig`.
