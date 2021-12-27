#include "space_object.h"

// TODO: unit tests in C++
space_object::space_object(pair field_size, pair position, pair size, int speed): position(position), speed(speed) {
    this->field_size = field_size;
    this->size = size;
}

void space_object::move() {
    position = pair(position.x, position.y + speed);
}

bool space_object::is_flown_away() const {
    return position.y >= field_size.y;
}

// TODO: should take space_object
bool space_object::is_collided_with_object(pair object_position, pair object_size) const {
    int x = position.x;
    int y = position.y;
    int object_x = object_position.x;
    int object_y = object_position.y;
    return intersect(pair(x, x + size.x), pair(object_x, object_x + object_size.x)) && intersect(pair(y, y + size.y), pair(object_y, object_y + object_size.y));
}

bool space_object::intersect(pair segment_a, pair segment_b) {
    return segment_b.x <= segment_a.y && segment_a.x <= segment_b.y;
}
