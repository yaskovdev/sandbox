#ifndef CPP_SPACE_GAME_PLAYER_H
#define CPP_SPACE_GAME_PLAYER_H


#include "pair.h"
#include "clock.h"
#include "space_object.h"

class player : public space_object {
public:
    int health = 5;
    bool collided = false;
    unsigned int most_recent_collision_time = 0;

    explicit player(class clock const &clock, pair position, pair size);

    void apply_collision_damage();

    bool is_dead() const;

    bool collided_recently() const;

private:
    class clock const &clock_;
};


#endif //CPP_SPACE_GAME_PLAYER_H
