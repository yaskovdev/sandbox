#ifndef CPP_SNAKE_GAME_GAME_H
#define CPP_SNAKE_GAME_GAME_H

#include <utility>
#include <SDL_keycode.h>
#include <unordered_map>

class game {
public:
    std::unordered_map<int, std::pair<int, int>> key_to_delta;
    bool ongoing;
    std::pair<int, int> position;

    game();

    void handle_keydown(int key);

    void quit();
};

#endif //CPP_SNAKE_GAME_GAME_H
