#include <cstdlib>
#include "enemy.h"

// TODO: unit tests in C++
enemy::enemy(pair field_size, pair size) {
    this->field_size = field_size;
    this->size = size;
    this->position = pair(rand() % (field_size.x - size.x), 0);
}

void enemy::move() {
    int y = position.y + 1;
    position = pair(position.x, y);
}
