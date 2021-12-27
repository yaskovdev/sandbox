#include "game.h"
#include "enemy.h"

#define PLAYER_SPEED 1

game::game() {
    key_to_delta = std::unordered_map<int, pair>({
        {SDLK_UP,    pair(0, -PLAYER_SPEED)},
        {SDLK_DOWN,  pair(0, PLAYER_SPEED)},
        {SDLK_LEFT,  pair(-PLAYER_SPEED, 0)},
        {SDLK_RIGHT, pair(PLAYER_SPEED, 0)}
    });
    ongoing = true;
    field_size = pair(720, 480);
    player_size = pair(25, 100);
    player_position = pair(0, 0);
    player_health = 100;
    pressed_keys = std::unordered_set<int>();
    time = 0;
    collided = false;
    most_recent_collision_time = 0;
}

void game::handle_keydown(int key) {
    pressed_keys.insert(key);
}

void game::handle_keyup(int key) {
    pressed_keys.erase(key);
}

void game::tick() {
    for (int const pressed_key: pressed_keys) {
        pair delta = key_to_delta[pressed_key];
        player_position.x = bounded(player_position.x + delta.x, 0, field_size.x - player_size.x);
        player_position.y = bounded(player_position.y + delta.y, 0, field_size.y - player_size.y);
    }

    for (enemy &enemy: enemies) {
        enemy.move();
    }
    update_state_of_enemies();

    if (time % 100 == 0 && rand() % 2 == 0) {
        enemy enemy(field_size, pair(20, 20));
        enemies.push_back(enemy);
    }
    time += 1;
}

void game::update_state_of_enemies() {
    std::list<enemy>::const_iterator enemy = enemies.begin();
    while (enemy != enemies.end()) {
        if (enemy->is_flown_away()) {
            enemy = enemies.erase(enemy);
        } else if (enemy->is_collided_with_object(player_position, player_size)) {
            player_health -= 1;
            collided = true;
            most_recent_collision_time = std::max(most_recent_collision_time, time);
            enemy = enemies.erase(enemy);
        } else {
            ++enemy;
        }
    }
}

void game::quit() {
    ongoing = false;
}

int game::bounded(int value, int min, int max) {
    return std::min(std::max(value, min), max);
}

bool game::collided_recently() const {
    return collided && time - most_recent_collision_time <= 500; // TODO: introduce class player
}
