namespace UniversalRegisterMachineInterpreter;

/// <summary>
/// Registers can be from 1 to 5 inclusively.
/// </summary>
public class LimitedInterpreter
{
    public void Interpret(int[] memory)
    {
        while (true)
        {
            if (memory[6 + memory[60]] == 0)
            {
                break;
            }
            else if (memory[6 + memory[60]] == -1)
            {
                memory[60] += 1;
                memory[memory[6 + memory[60]]] += 1;
            }
            else if (memory[6 + memory[60]] == -2)
            {
                memory[60] += 1;
                memory[memory[6 + memory[60]]] -= 1;
            }
            else if (memory[6 + memory[60]] == -3)
            {
                memory[61] = 1;
                while (memory[61] > 0)
                {
                    memory[60]++;
                    switch (memory[6 + memory[60]])
                    {
                        case -3:
                            memory[61]++;
                            break;
                        case -4:
                            memory[61]--;
                            break;
                    }
                }
        
                memory[60]--;
            }
            else if (memory[6 + memory[60]] == -4)
            {
                if (memory[memory[6 + memory[60] + 1]] > 0)
                {
                    memory[61] = 1;
                    while (memory[61] > 0)
                    {
                        memory[60]--;
                        switch (memory[6 + memory[60]])
                        {
                            case -4:
                                memory[61]++;
                                break;
                            case -3:
                                memory[61]--;
                                break;
                        }
                    }
                }
                else
                {
                    memory[60]++;
                }
            }
        
            memory[60]++;
        }
    }
}