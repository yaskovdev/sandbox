#include <algorithm>
#include "player.h"
#include "game_clock.h"

player::player(game_clock const &clock, pair position, pair size) : clock_(clock), space_object(position, size) {}

void player::apply_collision_damage() {
    health -= 1;
    collided_ = true;
    most_recent_collision_time_ = std::max(most_recent_collision_time_, clock_.time);
}

bool player::collided_recently() const {
    return collided_ && clock_.time - most_recent_collision_time_ <= 100;
}

bool player::is_dead() const {
    return health <= 0;
}
