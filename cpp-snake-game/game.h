#ifndef CPP_SNAKE_GAME_GAME_H
#define CPP_SNAKE_GAME_GAME_H

#include <utility>
#include <SDL_keycode.h>
#include <unordered_map>
#include <unordered_set>
#include <list>
#include "pair.h"
#include "space_object.h"
#include "player.h"

#define PLAYER_SPEED 1

class game {
public:
    std::unordered_map<int, pair> key_to_movement = std::unordered_map<int, pair>({
        {SDLK_UP,    pair(0, -PLAYER_SPEED)},
        {SDLK_DOWN,  pair(0, PLAYER_SPEED)},
        {SDLK_LEFT,  pair(-PLAYER_SPEED, 0)},
        {SDLK_RIGHT, pair(PLAYER_SPEED, 0)}
    });
    bool ongoing = true;
    bool paused = false;
    pair field_size = pair(720, 480);
    std::unordered_set<int> pressed_keys = std::unordered_set<int>();
    player player_;
    std::list<space_object> enemies;
    std::list<space_object> bullets;

    explicit game(class clock &clock);

    void handle_keydown(int key);

    void handle_keyup(int key);

    void tick();

    void quit();

    unsigned int time() const;

private:
    class clock &clock_;

    void update_state_of_bullets();

    void update_state_of_enemies();

    static int bounded(int value, int min, int max);

    static pair player_position(pair field_size, pair player_size);

    bool is_collided_with_bullet(std::list<space_object, std::allocator<space_object>>::const_iterator enemy);
};

#endif //CPP_SNAKE_GAME_GAME_H
