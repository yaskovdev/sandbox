#include "game.h"
#include "space_object.h"

#define PLAYER_SIZE pair(25, 100)
#define SHOOT_PERIOD 50
#define BULLET_SPEED pair (0, -2)

game::game(class clock &clock) : clock_(clock), player_(clock, PLAYER_SIZE, player_position(field_size, PLAYER_SIZE)) {}

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
                space_object bullet(bullet_position, pair(5, 5), BULLET_SPEED);
                bullets.push_back(bullet);
            }
        } else {
            pair delta = key_to_movement[pressed_key];
            player_.position.x = bounded(player_.position.x + delta.x, 0, field_size.x - player_.size.x);
            player_.position.y = bounded(player_.position.y + delta.y, 0, field_size.y - player_.size.y);
        }
    }

    for (space_object &bullet: bullets) {
        bullet.move();
    }
    update_state_of_bullets();

    for (space_object &enemy: enemies) {
        enemy.move();
    }
    update_state_of_enemies();

    if (clock_.time % 100 == 0 && rand() % 2 == 0) {
        const pair enemy_size = pair(20, 20);
        space_object enemy(pair(rand() % (field_size.x - enemy_size.x), 0), enemy_size, pair(0, 1));
        enemies.push_back(enemy);
    }

    if (player_.is_dead()) {
        paused = true;
    }

    clock_.tick();
}

void game::update_state_of_bullets() {
    std::list<space_object>::const_iterator bullet = bullets.begin();
    while (bullet != bullets.end()) {
        if (is_flown_away(*bullet)) {
            bullet = bullets.erase(bullet);
        } else {
            ++bullet;
        }
    }
}

void game::update_state_of_enemies() {
    std::list<space_object>::const_iterator enemy = enemies.begin();
    while (enemy != enemies.end()) {
        if (is_flown_away(*enemy)) {
            enemy = enemies.erase(enemy);
            // TODO: player should extend space_object
        } else if (enemy->is_collided_with_object(space_object(player_.position, player_.size, pair(0, 0)))) {
            enemy = enemies.erase(enemy);
            player_.apply_collision_damage();
        } else if (is_collided_with_bullet(enemy)) {
            enemy = enemies.erase(enemy);
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

bool game::is_collided_with_bullet(std::list<space_object, std::allocator<space_object>>::const_iterator enemy) {
    return std::any_of(bullets.begin(), bullets.end(), [&enemy](space_object b) { return enemy->is_collided_with_object(b); });
}
