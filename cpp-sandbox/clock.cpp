//
// Created by Sergey Yaskov on 27.12.2021.
//

#include "clock.h"

clock::clock() {
    _time = 0;
}

void clock::tick() {
    _time += 1;
}

unsigned int clock::get_time() {
    return _time;
}
