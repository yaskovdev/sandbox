#ifndef CPP_SNAKE_GAME_GAME_H
#define CPP_SNAKE_GAME_GAME_H

#include <utility>
#include <SDL_keycode.h>
#include <unordered_map>
#include <unordered_set>
#include "pair.h"

class game {
public:
    std::unordered_map<int, pair> key_to_delta;
    bool ongoing;
    pair field_size;
    pair player_size;
    pair player_position;
    std::unordered_set<int> pressed_keys;

    game();

    void handle_keydown(int key);

    void handle_keyup(int key);

    void tick();

    void quit();

    static int bounded(int value, int min, int max);

private:
    int time; // TODO: temporary way
};

#endif //CPP_SNAKE_GAME_GAME_H
