namespace UniversalRegisterMachineInterpreter;

public class FullyFunctionalInterpreter
{
    /// <summary>
    /// Note: the registers are 1-indexed.
    /// </summary>
    public void Interpret(string code, int[] registers)
    {
        Console.WriteLine("Before: " + string.Join(" ", registers));
        var pointer = 0;
        while (code[pointer] != '.')
        {
            if (code[pointer] == 'a')
            {
                pointer += 1;
                registers[code[pointer] - '0' - 1] += 1;
            }
            else if (code[pointer] == 's')
            {
                pointer += 1;
                registers[code[pointer] - '0' - 1] -= 1;
            }
            else if (code[pointer] == '(')
            {
                var b = 1;
                while (b > 0)
                {
                    pointer++;
                    switch (code[pointer])
                    {
                        case '(':
                            b++;
                            break;
                        case ')':
                            b--;
                            break;
                    }
                }

                pointer--;
            }
            else if (code[pointer] == ')')
            {
                var register = registers[code[pointer + 1] - '0' - 1];
                if (register > 0)
                {
                    var b = 1;
                    while (b > 0)
                    {
                        pointer--;
                        switch (code[pointer])
                        {
                            case ')':
                                b++;
                                break;
                            case '(':
                                b--;
                                break;
                        }
                    }
                }
                else
                {
                    pointer++;
                }
            }

            pointer += 1;
        }

        Console.WriteLine("After: " + string.Join(" ", registers));
    }
}