#ifndef CPP_SNAKE_GAME_PLAYER_H
#define CPP_SNAKE_GAME_PLAYER_H


#include "pair.h"

class player {
public:
    pair size;
    pair position;
    int health;
    bool collided;
    unsigned int most_recent_collision_time;

    player();
};


#endif //CPP_SNAKE_GAME_PLAYER_H
