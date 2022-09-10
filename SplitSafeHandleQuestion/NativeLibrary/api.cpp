#include "api.h"
#include <iostream>

int fill_two_arrays(uint8_t* xs, uint8_t* ys, const uint8_t number_of_elements_in_each_array)
{
    std::cout << "Native: starting to fill the arrays..." << "\n";
    for (uint8_t i = 0; i < number_of_elements_in_each_array; ++i)
    {
        xs[i] = i;
        ys[i] = i;
    }
    for (uint8_t i = 0; i < number_of_elements_in_each_array; ++i)
    {
        std::cout << "Native: " << static_cast<unsigned>(xs[i]) << ", " << static_cast<unsigned>(ys[i]) << "\n";
    }
    return 0;
}
