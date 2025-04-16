#include "api.h"
#include <iostream>

void fill_array()
{
    int* arr = new int[10];
    arr[10] = 42;
    std::cout << "Heap corrupted, but no crash yet\n";
    delete[] arr;
}
