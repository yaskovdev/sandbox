#include "game.h"
#include "enemy.h"

#define SPEED 2

game::game() {
    key_to_delta = std::unordered_map<int, pair>({
        {SDLK_UP,    pair(0, -SPEED)},
        {SDLK_DOWN,  pair(0, SPEED)},
        {SDLK_LEFT,  pair(-SPEED, 0)},
        {SDLK_RIGHT, pair(SPEED, 0)}
    });
    ongoing = true;
    field_size = pair(640, 480);
    player_size = pair(25, 100);
    player_position = pair(0, 0);
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
        player_position.x = bounded(player_position.x + delta.x, 0, field_size.x - player_size.x);
        player_position.y = bounded(player_position.y + delta.y, 0, field_size.y - player_size.y);
    }
    if (time % 10 == 0) {
        enemy enemy(field_size, pair(10, 10));
    }
    time += 1;
}

void game::quit() {
    ongoing = false;
}

int game::bounded(int value, int min, int max) {
    return std::min(std::max(value, min), max);
}
