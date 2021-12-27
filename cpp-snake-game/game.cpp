#include "game.h"
#include "space_object.h"
#include "moving_space_object.h"

game::game(class clock &clock) : clock_(clock), player_(clock, player_position(field_size, PLAYER_SIZE), PLAYER_SIZE) {}

void game::handle_keydown(int key) {
    pressed_keys.insert(key);
}

void game::handle_keyup(int key) {
    pressed_keys.erase(key);
}

void game::tick() {
    if (paused) {
        return;
    }

    for (int const pressed_key: pressed_keys) {
        if (pressed_key == SDLK_SPACE) {
            if (clock_.time % SHOOT_PERIOD == 0) {
                pair bullet_position = pair(player_.position.x + player_.size.x / 2 - 1, player_.position.y);
                moving_space_object bullet(bullet_position, pair(5, 5), BULLET_SPEED);
                bullets.push_back(bullet);
            }
        } else {
            pair delta = key_to_movement[pressed_key];
            player_.position.x = bounded(player_.position.x + delta.x, 0, field_size.x - player_.size.x);
            player_.position.y = bounded(player_.position.y + delta.y, 0, field_size.y - player_.size.y);
        }
    }

    for (moving_space_object &bullet: bullets) {
        bullet.move();
    }
    update_state_of_bullets();

    for (moving_space_object &enemy: enemies) {
        enemy.move();
    }
    update_state_of_enemies();

    if (clock_.time % 100 == 0 && rand() % 2 == 0) {
        const pair enemy_size = pair(20, 20);
        moving_space_object enemy(pair(rand() % (field_size.x - enemy_size.x), -enemy_size.y), enemy_size, pair(0, 1));
        enemies.push_back(enemy);
    }

    if (player_.is_dead()) {
        paused = true;
    }

    clock_.tick();
}

void game::update_state_of_bullets() {
    std::list<moving_space_object>::const_iterator bullet = bullets.begin();
    while (bullet != bullets.end()) {
        if (is_flown_away(*bullet)) {
            bullet = bullets.erase(bullet);
        } else {
            ++bullet;
        }
    }
}

void game::update_state_of_enemies() {
    std::list<moving_space_object>::const_iterator enemy = enemies.begin();
    while (enemy != enemies.end()) {
        if (is_flown_away(*enemy) || is_collided_with_bullet(*enemy)) {
            enemy = enemies.erase(enemy);
        } else if (enemy->is_collided_with_object(player_)) {
            enemy = enemies.erase(enemy);
            player_.apply_collision_damage();
        } else {
            ++enemy;
        }
    }
}

void game::quit() {
    ongoing = false;
}

unsigned int game::time() const {
    return clock_.time;
}

bool game::is_flown_away(space_object object) const {
    return object.position.y >= field_size.y;
}

int game::bounded(int value, int min, int max) {
    return std::min(std::max(value, min), max);
}

pair game::player_position(pair field_size, pair player_size) {
    return {field_size.x / 2 - player_size.x / 2 - 1, field_size.y / 2};
}

bool game::is_collided_with_bullet(space_object enemy) {
    return std::any_of(bullets.begin(), bullets.end(), [&](space_object b) { return enemy.is_collided_with_object(b); });
}
