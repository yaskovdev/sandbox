#ifndef CPP_SNAKE_GAME_GAME_H
#define CPP_SNAKE_GAME_GAME_H

#include <utility>
#include <SDL_keycode.h>
#include <unordered_map>
#include <unordered_set>
#include <list>
#include "pair.h"
#include "enemy.h"

class game {
public:
    std::unordered_map<int, pair> key_to_delta;
    unsigned int time;
    bool ongoing;
    pair field_size;
    pair player_size;
    pair player_position;
    int player_health;
    std::unordered_set<int> pressed_keys;
    std::list<enemy> enemies;

    game();

    void handle_keydown(int key);

    void handle_keyup(int key);

    void tick();

    void quit();

    static int bounded(int value, int min, int max);

private:

    void cleanup_flown_away_enemies();

    void cleanup_collided_enemies();
};

#endif //CPP_SNAKE_GAME_GAME_H
