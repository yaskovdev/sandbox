#include "moving_space_object.h"

moving_space_object::moving_space_object(pair position, pair size, pair speed) : space_object(position, size), speed(speed) {}

void moving_space_object::move() {
    position = pair(position.x + speed.x, position.y + speed.y);
}
