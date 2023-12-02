# Benchmarks

```
| Method                    | Mean        | Error      | StdDev     |
|-------------------------- |------------:|-----------:|-----------:|
| NoLockService             |   0.7190 ns |  0.0455 ns |  0.0426 ns |
| LockService               |  20.0909 ns |  0.3651 ns |  0.3585 ns |
| AsyncLockService          | 151.5001 ns |  3.0079 ns |  4.7709 ns |
| SemaphoreService          | 609.0784 ns | 12.1923 ns | 18.6190 ns |
| SemaphoreSlimService      |  45.0697 ns |  0.8985 ns |  1.3988 ns |
| AsyncSemaphoreSlimService |  61.9391 ns |  1.2692 ns |  2.0496 ns |
```
