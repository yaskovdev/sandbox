namespace UniversalRegisterMachineInterpreter;

public class LimitedInterpreter
{
    /// <summary>
    /// The memory should have the following layout ([REGISTERS][CODE][VARIABLES]): [0...5][6...59][60...61].
    /// Registers in the code can be from 1 to 5 inclusively.
    /// The registers in the memory are from 0 to 5 inclusively (i.e., the register with index 0 is never used).
    /// </summary>
    public void Interpret(int[] memory)
    {
        while (true)
        {
            if (memory[6 + memory[60]] == 0)
            {
                return;
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