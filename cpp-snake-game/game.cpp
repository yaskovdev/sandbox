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
    position = std::make_pair(0, 0);
}

void game::handle_keydown(int key) {
    std::pair<int, int> delta = key_to_delta[key];
    position.first += delta.first;
    position.second += delta.second;
}

void game::quit() {
    ongoing = false;
}
