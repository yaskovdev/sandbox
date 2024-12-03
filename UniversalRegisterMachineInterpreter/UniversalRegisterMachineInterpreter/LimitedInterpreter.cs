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
            if (memory[100 + memory[201]] == 0)
            {
                break;
            }
            else if (memory[100 + memory[201]] == -1)
            {
                memory[201] += 1;
                memory[memory[100 + memory[201]]] += 1;
            }
            else if (memory[100+ memory[201]] == -2)
            {
                memory[201] += 1;
                memory[memory[100 + memory[201]]] -= 1;
            }
            else if (memory[100 + memory[201]] == -3)
            {
                memory[202] = 1;
                while (memory[202] > 0)
                {
                    memory[201]++;
                    switch (memory[100 + memory[201]])
                    {
                        case -3:
                            memory[202]++;
                            break;
                        case -4:
                            memory[202]--;
                            break;
                    }
                }
        
                memory[201]--;
            }
            else if (memory[100 + memory[201]] == -4)
            {
                if (memory[memory[100 + memory[201] + 1]] > 0)
                {
                    memory[202] = 1;
                    while (memory[202] > 0)
                    {
                        memory[201]--;
                        switch (memory[100 + memory[201]])
                        {
                            case -4:
                                memory[202]++;
                                break;
                            case -3:
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
    }
}