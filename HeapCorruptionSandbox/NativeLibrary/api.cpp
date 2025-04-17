#include "api.h"
#include <iostream>

void corrupt_heap()
{
    int* arr = new int[4];
    arr[4] = 42;
    std::cout << "Heap corrupted, but no crash yet\n";
    delete[] arr;
    std::cout << "Deleted arr\n";
}
