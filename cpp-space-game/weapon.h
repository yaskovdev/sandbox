#ifndef CPP_SPACE_GAME_WEAPON_H
#define CPP_SPACE_GAME_WEAPON_H


#include <list>
#include "moving_space_object.h"

#define BULLET_SPEED pair(0, -10)

class weapon {
public:
    std::list<moving_space_object> shoot(pair weapon_position);
};


#endif //CPP_SPACE_GAME_WEAPON_H
