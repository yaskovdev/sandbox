#ifndef CPP_SNAKE_GAME_GAME_H
#define CPP_SNAKE_GAME_GAME_H

#include <utility>
#include <SDL_keycode.h>
#include <unordered_map>
#include <unordered_set>
#include <list>
#include "pair.h"
#include "enemy.h"
#include "player.h"

class game {
public:
    std::unordered_map<int, pair> key_to_movement;
    unsigned int time; // TODO: probably should be a clock object injected into game and player (and other classes if needed)
    bool ongoing;
    pair field_size;
    std::unordered_set<int> pressed_keys;
    player player;
    std::list<enemy> enemies;

    game();

    void handle_keydown(int key);

    void handle_keyup(int key);

    void tick();

    void quit();

    static int bounded(int value, int min, int max);

private:

    void update_state_of_enemies();
};

#endif //CPP_SNAKE_GAME_GAME_H
