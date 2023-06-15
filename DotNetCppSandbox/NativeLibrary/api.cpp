#include "api.h"
#include <iostream>

multiplier* create_multiplier(const int coefficient)
{
    std::cout << "Native: creating multiplier..." << "\n";
    return new multiplier(coefficient);
}

int multiply(const multiplier* multiplier, const int number)
{
    return multiplier->multiply(number);
}

void destroy_multiplier(const multiplier* multiplier)
{
    delete multiplier;
    std::cout << "Native: multiplier deleted" << "\n";
}

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
