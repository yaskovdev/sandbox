### Steps To Reproduce

1. Clone the solution.
2. Open PowerShell in the same directory as `BenchmarkDotNetBug.sln` as an admin.
3. Run `.\BuildAndRun.ps1`.
4. Wait a few minutes for the benchmark to finish.

Expected: both "Allocated native memory" and "Native memory leak" have "10 B" value.
Actual: both "Allocated native memory" and "Native memory leak" have "-" value.

Remove `KeepBenchmarkFiles` annotation from `MemoryLeak` class and run again. This time everything works as expected.
