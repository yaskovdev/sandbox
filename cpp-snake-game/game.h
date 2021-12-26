#ifndef CPP_SNAKE_GAME_GAME_H
#define CPP_SNAKE_GAME_GAME_H

#include <utility>
#include <SDL_keycode.h>
#include <unordered_map>
#include <unordered_set>

class game {
public:
    std::unordered_map<int, std::pair<int, int>> key_to_delta;
    bool ongoing;
    std::pair<int, int> field_size;
    std::pair<int, int> player_size;
    std::pair<int, int> player_position;
    std::unordered_set<int> pressed_keys;

    game();

    void handle_keydown(int key);

    void handle_keyup(int key);

    void update();

    void quit();
};

#endif //CPP_SNAKE_GAME_GAME_H
