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
    std::cout << "Native: multiplier deleted..." << "\n";
}
