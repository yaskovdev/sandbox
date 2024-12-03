namespace UniversalRegisterMachineInterpreter;

public class LimitedInterpreter
{
    public void Interpret(string code)
    {
        var memory = new int[203]; // [REGISTERS][STACK][VARIABLES]: [0...99][100...200][201...202] 
        memory[2 - 1] = 6;
        memory[3 - 1] = 3;
        Console.WriteLine("Before: " + string.Join(", ", memory));
        while (code[memory[201]] != '.')
        {
            if (code[memory[201]] == 'a')
            {
                memory[201] += 1;
                memory[code[memory[201]] - '0' - 1] += 1;
            }
            else if (code[memory[201]] == 's')
            {
                memory[201] += 1;
                memory[code[memory[201]] - '0' - 1] -= 1;
            }
            else if (code[memory[201]] == '(')
            {
                memory[202] = 1;
                while (memory[202] > 0)
                {
                    memory[201]++;
                    switch (code[memory[201]])
                    {
                        case '(':
                            memory[202]++;
                            break;
                        case ')':
                            memory[202]--;
                            break;
                    }
                }
        
                memory[201]--;
            }
            else if (code[memory[201]] == ')')
            {
                if (memory[code[memory[201] + 1] - '0' - 1] > 0)
                {
                    memory[202] = 1;
                    while (memory[202] > 0)
                    {
                        memory[201]--;
                        switch (code[memory[201]])
                        {
                            case ')':
                                memory[202]++;
                                break;
                            case '(':
                                memory[202]--;
                                break;
                        }
                    }
                }
                else
                {
                    memory[201]++;
                }
            }
        
            memory[201]++;
        }
        
        Console.WriteLine("After: " + string.Join(", ", memory));
    }
}