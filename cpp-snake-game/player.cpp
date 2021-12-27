#include <algorithm>
#include "player.h"
#include "clock.h"

player::player(class clock &clock, pair size, pair position) : clock_(clock), size(size), position_(position) {}

void player::apply_collision_damage() {
    health -= 20;
    collided = true;
    most_recent_collision_time = std::max(most_recent_collision_time, clock_.time);
}

bool player::collided_recently() const {
    return collided && clock_.time - most_recent_collision_time <= 500;
}

bool player::is_dead() const {
    return health <= 0;
}
