#pragma once

#include <cstdint>

#include "multiplier.h"

extern "C" __declspec(dllexport) multiplier* create_multiplier(int coefficient);

extern "C" __declspec(dllexport) int multiply(const multiplier* multiplier, int number);

extern "C" __declspec(dllexport) void destroy_multiplier(const multiplier* multiplier);

extern "C" __declspec(dllexport) void fill_array(uint8_t* array);
