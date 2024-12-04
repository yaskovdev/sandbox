# Universal Register Machine (URM) Interpreter

See this page, "URM is BF-complete" for more details about the Universal Register Machine.

The `FullyFunctionalInterpreter` is an interpreter of a URM with registers ranging from 0 to 9 inclusively.

The `LimitedInterpreter` is an interpreter of a URM with five registers. It accepts a program encoded in this way:

| URM Symbol | Encoding |
|------------|----------|
| .          | 0        |
| a          | -1       |
| s          | -2       |
| (          | -3       |
| )          | -4       |
| 1          | 1        |
| 2          | 2        |
| 3          | 3        |
| 4          | 4        |
| 5          | 5        |

The `LimitedInterpreter` is a step towards a Push implementation of a URM.

See https://gist.github.com/yaskovdev/71010ede2d070ed88c11334160fedc88 for a Push implementation.
