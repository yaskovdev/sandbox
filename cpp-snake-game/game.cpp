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
    pressed_keys = std::unordered_set<int>();
    time = 0;
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
        player.position.x = bounded(player.position.x + delta.x, 0, field_size.x - player.size.x);
        player.position.y = bounded(player.position.y + delta.y, 0, field_size.y - player.size.y);
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
        } else if (enemy->is_collided_with_object(player.position, player.size)) {
            // TODO: should be a method of player
            player.health -= 1;
            player.collided = true;
            player.most_recent_collision_time = std::max(player.most_recent_collision_time, time);
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

// TODO: should be a method of player
bool game::collided_recently() const {
    return player.collided && time - player.most_recent_collision_time <= 500;
}
