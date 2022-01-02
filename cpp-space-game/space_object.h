#ifndef CPP_SPACE_GAME_SPACE_OBJECT_H
#define CPP_SPACE_GAME_SPACE_OBJECT_H

#include "pair.h"

class space_object {
public:
    pair position;

    pair size;

    space_object(pair position, pair size);

    [[nodiscard]] bool is_collided_with(space_object &other) const;
};

#endif //CPP_SPACE_GAME_SPACE_OBJECT_H
