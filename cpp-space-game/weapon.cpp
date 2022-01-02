#include "weapon.h"

std::list<moving_space_object> weapon::shoot(const pair weapon_position) {
    return {moving_space_object(weapon_position, pair(5, 5), BULLET_SPEED)};
}
