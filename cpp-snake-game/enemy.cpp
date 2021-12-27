#include <cstdlib>
#include "enemy.h"

// TODO: unit tests in C++
enemy::enemy(pair field_size, pair size) {
    this->field_size = field_size;
    this->size = size;
    this->position = pair(rand() % (field_size.x - size.x), 0);
    this->speed = 1;
}

void enemy::move() {
    position = pair(position.x, position.y + speed);
}

bool enemy::is_flown_away() const {
    return position.y >= field_size.y;
}

bool enemy::is_collided_with_object(pair object_position, pair object_size) const {
    int x = position.x;
    int y = position.y;
    int object_x = object_position.x;
    int object_y = object_position.y;
    return intersect(pair(x, x + size.x), pair(object_x, object_x + object_size.x)) && intersect(pair(y, y + size.y), pair(object_y, object_y + object_size.y));
}

bool enemy::intersect(pair segment_a, pair segment_b) {
    return segment_b.x <= segment_a.y && segment_a.x <= segment_b.y;
}
