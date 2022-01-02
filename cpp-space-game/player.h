#ifndef CPP_SPACE_GAME_PLAYER_H
#define CPP_SPACE_GAME_PLAYER_H


#include "pair.h"
#include "game_clock.h"
#include "space_object.h"

class player : public space_object {
public:
    int health = 5;

    explicit player(game_clock const &clock, pair position, pair size);

    void apply_collision_damage();

    [[nodiscard]] bool is_dead() const;

    [[nodiscard]] bool collided_recently() const;

private:
    game_clock const &clock_;
    bool collided_ = false;
    unsigned int most_recent_collision_time_ = 0;
};


#endif //CPP_SPACE_GAME_PLAYER_H
