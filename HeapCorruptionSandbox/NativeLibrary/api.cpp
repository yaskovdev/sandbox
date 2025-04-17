#include "api.h"
#include <iostream>

void corrupt_heap()
{
    constexpr int size = 16; // See README.md for why it's 16.
    char* arr = new char[size];
    arr[size] = 'x';
    std::cout << "Heap corrupted, but no crash yet\n";
    delete[] arr;
    std::cout << "Deleted arr\n";
}
