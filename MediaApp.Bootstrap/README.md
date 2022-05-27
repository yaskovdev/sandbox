Reproduces the situation described [here](https://github.com/dotnet/aspnetcore/issues/17221#issuecomment-555587359).

Just run the program and then press Ctrl+C to it to try to shut down it gradually. Then observe the crash with `System.OperationCanceledException`.
