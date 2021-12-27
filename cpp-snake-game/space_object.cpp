#include "space_object.h"

// TODO: unit tests in C++
space_object::space_object(pair position, pair size) : position(position), size(size) {}

bool space_object::is_collided_with_object(space_object object) const {
    int x = position.x;
    int y = position.y;
    int object_x = object.position.x;
    int object_y = object.position.y;
    return intersect(pair(x, x + size.x), pair(object_x, object_x + object.size.x)) && intersect(pair(y, y + size.y), pair(object_y, object_y + object.size.y));
}

bool space_object::intersect(pair segment_a, pair segment_b) {
    return segment_b.x <= segment_a.y && segment_a.x <= segment_b.y;
}
