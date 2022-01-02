#include "space_object.h"

space_object::space_object(pair position, pair size) : position(position), size(size) {}

// TODO: unit tests in C++
bool space_object::is_collided_with(space_object &other) const {
    int x = position.x;
    int y = position.y;
    int other_x = other.position.x;
    int other_y = other.position.y;
    return pair(x, x + size.x).intersects(pair(other_x, other_x + other.size.x)) && pair(y, y + size.y).intersects(pair(other_y, other_y + other.size.y));
}
