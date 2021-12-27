#include "game.h"
#include "enemy.h"

#define PLAYER_SIZE pair(25, 100)

game::game(class clock &clock) : clock_(clock), player_(clock, PLAYER_SIZE, player_position(field_size, PLAYER_SIZE)) {}

void game::handle_keydown(int key) {
    pressed_keys.insert(key);
}

void game::handle_keyup(int key) {
    pressed_keys.erase(key);
}

void game::tick() {
    if (paused) {
        return;
    }

    for (int const pressed_key: pressed_keys) {
        pair delta = key_to_movement[pressed_key];
        player_.position_.x = bounded(player_.position_.x + delta.x, 0, field_size.x - player_.size.x);
        player_.position_.y = bounded(player_.position_.y + delta.y, 0, field_size.y - player_.size.y);
    }

    for (enemy &enemy: enemies) {
        enemy.move();
    }
    update_state_of_enemies();

    if (clock_.time % 100 == 0 && rand() % 2 == 0) {
        enemy enemy(field_size, pair(20, 20));
        enemies.push_back(enemy);
    }

    if (player_.is_dead()) {
        paused = true;
    }

    clock_.tick();
}

void game::update_state_of_enemies() {
    std::list<enemy>::const_iterator enemy = enemies.begin();
    while (enemy != enemies.end()) {
        if (enemy->is_flown_away()) {
            enemy = enemies.erase(enemy);
        } else if (enemy->is_collided_with_object(player_.position_, player_.size)) {
            enemy = enemies.erase(enemy);
            player_.apply_collision_damage();
        } else {
            ++enemy;
        }
    }
}

void game::quit() {
    ongoing = false;
}

unsigned int game::time() const {
    return clock_.time;
}

int game::bounded(int value, int min, int max) {
    return std::min(std::max(value, min), max);
}

pair game::player_position(pair field_size, pair player_size) {
    return {field_size.x / 2 - player_size.x / 2 - 1, field_size.y / 2};
}
