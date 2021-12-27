#include <algorithm>
#include "player.h"
#include "clock.h"

player::player(class clock &clock, pair position, pair size) : clock_(clock), space_object(position, size) {}

void player::apply_collision_damage() {
    health -= 1;
    collided = true;
    most_recent_collision_time = std::max(most_recent_collision_time, clock_.time);
}

bool player::collided_recently() const {
    return collided && clock_.time - most_recent_collision_time <= 100;
}

bool player::is_dead() const {
    return health <= 0;
}
