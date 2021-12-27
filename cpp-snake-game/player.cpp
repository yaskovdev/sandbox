#include "player.h"

player::player() {
    size = pair(25, 100);
    position = pair(0, 0);
    health = 100;
    collided = false;
    most_recent_collision_time = 0;
}
