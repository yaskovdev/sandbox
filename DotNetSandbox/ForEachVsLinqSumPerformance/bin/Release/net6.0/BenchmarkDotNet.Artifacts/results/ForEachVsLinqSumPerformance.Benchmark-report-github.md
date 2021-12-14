``` ini

BenchmarkDotNet=v0.13.1, OS=macOS Big Sur 11.1 (20C69) [Darwin 20.2.0]
Intel Core i7-7820HQ CPU 2.90GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.100
  [Host]     : .NET 6.0.0 (6.0.21.52210), X64 RyuJIT
  DefaultJob : .NET 6.0.0 (6.0.21.52210), X64 RyuJIT


```
|     Method |     Mean |   Error |  StdDev |
|----------- |---------:|--------:|--------:|
| ForEachSum | 281.5 μs | 2.14 μs | 1.89 μs |
|    LinqSum | 263.9 μs | 2.50 μs | 2.34 μs |
