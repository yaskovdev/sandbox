#ifndef CPP_SNAKE_GAME_ENEMY_H
#define CPP_SNAKE_GAME_ENEMY_H

#include <utility>
#include "pair.h"

class enemy {
public:
    pair field_size;

    pair size;

    pair position;

    enemy(pair field_size, pair size);
};

#endif //CPP_SNAKE_GAME_ENEMY_H
