#ifndef CPP_SNAKE_GAME_SPACE_OBJECT_H
#define CPP_SNAKE_GAME_SPACE_OBJECT_H

#include "pair.h"

class space_object {
public:
    pair size;

    pair position;

    space_object(pair position, pair size);

    bool is_collided_with_object(space_object &object) const;

private:
    static bool intersect(pair segment_a, pair segment_b);
};

#endif //CPP_SNAKE_GAME_SPACE_OBJECT_H
