#ifndef CPP_SNAKE_GAME_PLAYER_H
#define CPP_SNAKE_GAME_PLAYER_H


#include "pair.h"
#include "clock.h"

class player {
public:
    pair size;
    pair position_;
    int health = 100;
    bool collided = false;
    unsigned int most_recent_collision_time = 0;

    explicit player(class clock &clock, pair size, pair position);

    void apply_collision_damage();

    bool is_dead() const;

    bool collided_recently() const;

private:
    class clock &clock_;
};


#endif //CPP_SNAKE_GAME_PLAYER_H
