# Benchmarks

```
| Method                | Mean        | Error      | StdDev     | Median      |
|---------------------- |------------:|-----------:|-----------:|------------:|
| NoLockService         |   0.7513 ns |  0.1014 ns |  0.2991 ns |   0.6164 ns |
| LockService           |  19.9286 ns |  0.3845 ns |  0.3211 ns |  19.9778 ns |
| AsyncLockService      | 150.0560 ns |  3.0314 ns |  5.6938 ns | 148.8215 ns |
| SemaphoreService      | 604.1106 ns | 11.7264 ns | 16.0513 ns | 601.9850 ns |
| AsyncSemaphoreService |  63.4153 ns |  1.3999 ns |  3.9941 ns |  61.8692 ns |
```
