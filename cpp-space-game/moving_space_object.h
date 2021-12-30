#ifndef CPP_SPACE_GAME_MOVING_SPACE_OBJECT_H
#define CPP_SPACE_GAME_MOVING_SPACE_OBJECT_H


#include "space_object.h"

class moving_space_object : public space_object {
public:
    moving_space_object(pair position, pair size, pair speed);

    void move();

private:
    pair speed_;
};


#endif //CPP_SPACE_GAME_MOVING_SPACE_OBJECT_H
