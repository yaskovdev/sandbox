#include "api.h"
#include <iostream>

void corrupt_heap()
{
    int* arr = new int[96];
    arr[96] = 42;
    std::cout << "Heap corrupted, but no crash yet\n";
    delete[] arr;
    std::cout << "Deleted arr\n";
}
