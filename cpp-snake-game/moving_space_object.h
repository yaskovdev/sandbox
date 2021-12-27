#ifndef CPP_SNAKE_GAME_MOVING_SPACE_OBJECT_H
#define CPP_SNAKE_GAME_MOVING_SPACE_OBJECT_H


#include "space_object.h"

class moving_space_object : public space_object {
public:
    pair speed;

    moving_space_object(pair position, pair size, pair speed);

    void move();
};


#endif //CPP_SNAKE_GAME_MOVING_SPACE_OBJECT_H
