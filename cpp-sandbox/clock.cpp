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
