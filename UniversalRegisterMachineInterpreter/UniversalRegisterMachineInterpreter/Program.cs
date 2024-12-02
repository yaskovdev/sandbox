// See https://iwriteiam.nl/Ha_bf_Turing.html for more information

var code = "(a2;s3)3.";
var registers = new int[100];
registers[2 - 1] = 1;
registers[3 - 1] = 3;
Console.WriteLine("Before: " + string.Join(", ", registers));
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
        while (code[pointer] != ')')
        {
            pointer++;
        }

        pointer--;
    }
    else if (code[pointer] == ')')
    {
        var register = registers[code[pointer + 1] - '0' - 1];
        if (register > 0)
        {
            while (code[pointer] != '(')
            {
                pointer--;
            }
        }
        else
        {
            pointer++;
        }
    }

    pointer += 1;
}

Console.WriteLine("After: " + string.Join(", ", registers));