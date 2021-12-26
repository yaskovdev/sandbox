#include "game.h"

#define SPEED 20

game::game() {
    key_to_delta = std::unordered_map<int, std::pair<int, int>>({
        {SDLK_UP,    std::make_pair(0, -SPEED)},
        {SDLK_DOWN,  std::make_pair(0, SPEED)},
        {SDLK_LEFT,  std::make_pair(-SPEED, 0)},
        {SDLK_RIGHT, std::make_pair(SPEED, 0)}
    });
    ongoing = true;
    field_size = std::make_pair(640, 480);
    player_size = std::make_pair(50, 50);
    player_position = std::make_pair(0, 0);
}

int bounded(int value, int min, int max) {
    return std::min(std::max(value, min), max);
}

void game::handle_keydown(int key) {
    std::pair<int, int> delta = key_to_delta[key];
    player_position.first = bounded(player_position.first + delta.first, 0, field_size.first - player_size.first);
    player_position.second = bounded(player_position.second + delta.second, 0, field_size.second - player_size.second);
}

void game::quit() {
    ongoing = false;
}
