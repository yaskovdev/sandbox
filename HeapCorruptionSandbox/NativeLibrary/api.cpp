#include "api.h"
#include <iostream>

void corrupt_heap()
{
    char* arr = new char[16];
    arr[16] = 'x';
    std::cout << "Heap corrupted, but no crash yet\n";
    delete[] arr;
    std::cout << "Deleted arr\n";
}
