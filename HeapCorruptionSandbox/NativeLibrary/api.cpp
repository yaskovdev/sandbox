#include "api.h"
#include <iostream>

void fill_array(uint8_t* array, const int length)
{
    std::cout << "Before filling:";
    for (int i = 0; i < length; i++)
    {
        std::cout << " " << static_cast<unsigned>(array[i]);
    }
    std::cout << "\n";

    for (int i = 0; i < length; i++)
    {
        array[i] = 1;
    }
}
