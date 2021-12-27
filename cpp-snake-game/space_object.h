#ifndef CPP_SNAKE_GAME_SPACE_OBJECT_H
#define CPP_SNAKE_GAME_SPACE_OBJECT_H

#include "pair.h"

class space_object {
public:
    pair size;

    pair position;

    pair speed;

    space_object(pair field_size, pair position, pair size, pair speed);

    bool is_flown_away() const;

    void move();

    bool is_collided_with_object(space_object object) const;

private:
    pair field_size;

    static bool intersect(pair segment_a, pair segment_b);
};

#endif //CPP_SNAKE_GAME_SPACE_OBJECT_H
