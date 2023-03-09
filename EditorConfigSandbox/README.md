Try building the project with:

```powershell
msbuild /t:"Clean" /t:EditorConfigSandbox:Rebuild
```

The build with fail with the next error: "error CA1036: RatingInformation should define operator(s) '==, !=, <, <=, >, >=' and Equals since it implements IComparable".

Now replace `error` with `warning` in `.editorconfig` and try building again with the same command. The build will succeed.

Outcome: the C# compiler is taking the .editorconfig into consideration.
