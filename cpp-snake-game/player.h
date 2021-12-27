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

    void apply_collision_damage(unsigned int collision_time);

    bool collided_recently(unsigned int time) const;
};


#endif //CPP_SNAKE_GAME_PLAYER_H
