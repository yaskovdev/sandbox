#include <algorithm>
#include "player.h"

player::player() {
    size = pair(25, 100);
    position = pair(0, 0);
    health = 100;
    collided = false;
    most_recent_collision_time = 0;
}

void player::apply_collision_damage(unsigned int collision_time) {
    health -= 1;
    collided = true;
    most_recent_collision_time = std::max(most_recent_collision_time, collision_time);
}

bool player::collided_recently(unsigned int time) const {
    return collided && time - most_recent_collision_time <= 500;
}
