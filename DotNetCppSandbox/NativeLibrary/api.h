#pragma once

#include "multiplier.h"

extern "C" __declspec(dllexport) multiplier* create_multiplier(int coefficient);

extern "C" __declspec(dllexport) int multiply(const multiplier* multiplier, int number);

extern "C" __declspec(dllexport) void destroy_multiplier(const multiplier* multiplier);
