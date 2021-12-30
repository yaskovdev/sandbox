#ifndef CPP_SPACE_GAME_GAME_H
#define CPP_SPACE_GAME_GAME_H

#include <utility>
#include <SDL_keycode.h>
#include <unordered_map>
#include <unordered_set>
#include <list>
#include <random>
#include "pair.h"
#include "space_object.h"
#include "player.h"
#include "moving_space_object.h"

#define PLAYER_SPEED 5
#define PLAYER_SIZE pair(25, 100)
#define SHOOT_PERIOD 20
#define BULLET_SPEED pair(0, -10)

class game {
public:
    bool stopped = false;

    bool paused = false;

    player player_;

    std::list<moving_space_object> enemies;

    std::list<moving_space_object> bullets;

    int score = 0;

    explicit game(game_clock &clock, std::mt19937 &generator, pair const &field_size);

    void handle_keydown(int key);

    void handle_keyup(int key);

    void tick();

    void quit();

    unsigned int time() const;

private:
    std::unordered_map<int, pair> key_to_movement = std::unordered_map<int, pair>({
        {SDLK_UP,    pair(0, -PLAYER_SPEED)},
        {SDLK_DOWN,  pair(0, PLAYER_SPEED)},
        {SDLK_LEFT,  pair(-PLAYER_SPEED, 0)},
        {SDLK_RIGHT, pair(PLAYER_SPEED, 0)}
    });

    game_clock &clock_;

    std::mt19937 &generator_;

    pair field_size_;

    std::unordered_set<int> pressed_keys_ = std::unordered_set<int>();

    unsigned int most_recent_shot_time_ = 0;

    void update_state_of_bullets();

    void update_state_of_enemies();

    bool is_collided_with_bullet(space_object const &bullet);

    bool is_flown_away(space_object const &object) const;

    static int bounded(int value, int min, int max);

    static pair player_position(pair const &field_size, pair const &player_size);
};

#endif //CPP_SPACE_GAME_GAME_H
