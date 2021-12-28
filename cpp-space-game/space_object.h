#ifndef CPP_SPACE_GAME_SPACE_OBJECT_H
#define CPP_SPACE_GAME_SPACE_OBJECT_H

#include "pair.h"

class space_object {
public:
    pair size;

    pair position;

    space_object(pair position, pair size);

    bool is_collided_with_object(space_object &object) const;
};

#endif //CPP_SPACE_GAME_SPACE_OBJECT_H
