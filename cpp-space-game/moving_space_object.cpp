#include "moving_space_object.h"

moving_space_object::moving_space_object(pair position, pair size, pair speed) : space_object(position, size), speed_(speed) {}

void moving_space_object::move() {
    position = pair(position.x + speed_.x, position.y + speed_.y);
}
