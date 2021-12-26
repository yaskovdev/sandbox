#ifndef CPP_SNAKE_GAME_ENEMY_H
#define CPP_SNAKE_GAME_ENEMY_H

#include <utility>
#include "pair.h"

class enemy {
public:
    pair size;

    pair position;

    int speed;

    bool collided;

    enemy(pair field_size, pair size);

    bool is_flown_away() const;

    void move();

    bool is_collided_with_object(pair object_size, pair object_position) const;

private:
    pair field_size;

    static bool intersect(pair segment_a, pair segment_b);
};

#endif //CPP_SNAKE_GAME_ENEMY_H
