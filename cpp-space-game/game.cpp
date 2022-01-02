#include <iostream>
#include "game.h"
#include "space_object.h"
#include "moving_space_object.h"

game::game(game_clock &clock, std::mt19937 &generator, const pair &field_size) :
    clock_(clock), generator_(generator), field_size_(field_size), player_(clock, initial_player_position(field_size, PLAYER_SIZE), PLAYER_SIZE) {}

void game::handle_keydown(const int key) {
    pressed_keys_.insert(key);
}

void game::handle_keyup(const int key) {
    pressed_keys_.erase(key);
}

void game::tick() {
    if (paused) {
        return;
    }

    for (const int pressed_key: pressed_keys_) {
        if (pressed_key == SDLK_SPACE && clock_.time - most_recent_shot_time_ >= SHOOT_PERIOD) {
            const std::list<moving_space_object> &shot_bullets = weapon_.shoot(pair(player_.position.x + player_.size.x / 2 - 1, player_.position.y));
            bullets.insert(bullets.end(), shot_bullets.begin(), shot_bullets.end());
            most_recent_shot_time_ = clock_.time;
        } else if (key_to_movement.count(pressed_key)) {
            const auto[dx, dy] = key_to_movement.at(pressed_key);
            player_.position.x = bounded(player_.position.x + dx, 0, field_size_.x - player_.size.x);
            player_.position.y = bounded(player_.position.y + dy, 0, field_size_.y - player_.size.y);
        }
    }

    if (score >= 10) {
        score = 0;
        player_.health += 1;
    }

    for (moving_space_object &bullet: bullets) {
        bullet.move();
    }
    update_state_of_bullets();

    for (moving_space_object &enemy: enemies) {
        enemy.move();
    }
    update_state_of_enemies();

    std::uniform_int_distribution<int> enemy_spawn_distribution(0, 1);
    std::uniform_int_distribution<int> enemy_moves_horizontally_distribution(0, 2);
    std::uniform_int_distribution<int> enemy_horizontal_speed_distribution(-1, 1);
    if (clock_.time % 10 == 0 && enemy_spawn_distribution(generator_) == 0) {
        const pair enemy_size = pair(20, 20);
        int horizontal_speed = enemy_moves_horizontally_distribution(generator_) == 0 ? enemy_horizontal_speed_distribution(generator_) : 0;
        std::uniform_int_distribution<int> enemy_position_distribution(0, field_size_.x - enemy_size.x - 1);
        pair enemy_position = pair(enemy_position_distribution(generator_), -enemy_size.y);
        moving_space_object enemy(enemy_position, enemy_size, pair(horizontal_speed, 5));
        enemies.push_back(enemy);
    }

    if (player_.is_dead()) {
        paused = true;
    }

    clock_.tick();
}

void game::update_state_of_bullets() {
    auto bullet = bullets.begin();
    while (bullet != bullets.end()) {
        if (is_flown_away(*bullet)) {
            bullet = bullets.erase(bullet);
        } else {
            ++bullet;
        }
    }
}

void game::update_state_of_enemies() {
    auto enemy = enemies.begin();
    while (enemy != enemies.end()) {
        if (is_flown_away(*enemy)) {
            enemy = enemies.erase(enemy);
        } else if (is_collided_with_bullet(*enemy)) {
            enemy = enemies.erase(enemy);
            score++;
        } else if (enemy->is_collided_with(player_)) {
            enemy = enemies.erase(enemy);
            player_.apply_collision_damage();
        } else {
            ++enemy;
        }
    }
}

void game::quit() {
    stopped = true;
}

unsigned int game::time() const {
    return clock_.time;
}

bool game::is_flown_away(const space_object &object) const {
    return object.position.y >= field_size_.y;
}

int game::bounded(const int value, const int min, const int max) {
    return std::min(std::max(value, min), max);
}

pair game::initial_player_position(const pair &field_size, const pair &player_size) {
    return {field_size.x / 2 - player_size.x / 2 - 1, field_size.y / 2};
}

bool game::is_collided_with_bullet(const space_object &enemy) {
    return std::any_of(bullets.begin(), bullets.end(), [&](space_object bullet) { return enemy.is_collided_with(bullet); });
}
