#include "clock.h"

clock::clock() {
    time_ = 0;
}

void clock::tick() {
    time_ += 1;
}

unsigned int clock::get_time() const {
    return time_;
}
