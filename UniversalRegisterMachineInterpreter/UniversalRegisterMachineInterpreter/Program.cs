// See https://iwriteiam.nl/Ha_bf_Turing.html for more information

var code = "(a4;a5;s2)2; ((a2;s4)4; s3; (a1;a4;s5)5; (a5;s1)1)3.";
var registers = new int[100];
registers[2 - 1] = 6;
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

Console.WriteLine("After: " + string.Join(", ", registers));