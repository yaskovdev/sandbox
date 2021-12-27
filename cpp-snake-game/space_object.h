#ifndef CPP_SNAKE_GAME_SPACE_OBJECT_H
#define CPP_SNAKE_GAME_SPACE_OBJECT_H

#include <utility>
#include "pair.h"

class space_object {
public:
    pair size;

    pair position;

    int speed;

    space_object(pair field_size, pair position, pair size, int speed);

    bool is_flown_away() const;

    void move();

    bool is_collided_with_object(pair object_position, pair object_size) const;

private:
    pair field_size;

    static bool intersect(pair segment_a, pair segment_b);
};

#endif //CPP_SNAKE_GAME_SPACE_OBJECT_H
