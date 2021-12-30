#include <iostream>
#include <chrono>
#include <random>
#include "SDL.h"
#include "game.h"
#include "pair.h"
#include "color.h"

#define HEALTH_BAR_COLOR color(255, 0, 255)
#define SCORE_BAR_COLOR color(0, 255, 255)

using std::cout;
using std::endl;

void render_rectangle(SDL_Renderer *const renderer, pair const position, pair const size, color const color) {
    SDL_Rect rectangle;
    rectangle.x = position.x;
    rectangle.y = position.y;
    rectangle.w = size.x;
    rectangle.h = size.y;
    SDL_SetRenderDrawColor(renderer, color.r, color.g, color.b, SDL_ALPHA_OPAQUE);
    SDL_RenderFillRect(renderer, &rectangle);
}

bool visible = true;
unsigned int most_recent_visibility_change = 0;

void render_player(SDL_Renderer *const renderer, game const &game) {
    if (!game.paused && game.player_.collided_recently()) {
        if (game.time() - most_recent_visibility_change > 10) {
            visible = !visible;
            most_recent_visibility_change = game.time();
        }
    } else {
        visible = true;
    }
    if (visible) {
        render_rectangle(renderer, game.player_.position, game.player_.size, color(255, 255, 255));
    }
}

void render_health_bar(SDL_Renderer *const renderer, game const &game) {
    int health = game.player_.health;
    for (int i = 0; i < health; i++) {
        render_rectangle(renderer, pair(10 + 20 * i, 10), pair(10, 10), HEALTH_BAR_COLOR);
    }
}

void render_score_bar(SDL_Renderer *const renderer, game const &game) {
    for (int i = 0; i < game.score; i++) {
        render_rectangle(renderer, pair(10 + 20 * i, 30), pair(10, 10), SCORE_BAR_COLOR);
    }
}

bool quit_requested(const SDL_Event e) {
    return e.type == SDL_QUIT || (e.type == SDL_KEYDOWN && e.key.keysym.sym == SDLK_ESCAPE);
}

long long now_us() {
    return std::chrono::duration_cast<std::chrono::microseconds>(std::chrono::system_clock::now().time_since_epoch()).count();
};

int main(int const argc, char const *const argv[]) {
    SDL_Init(SDL_INIT_VIDEO);
    SDL_DisplayMode display_mode;
    int const status = SDL_GetCurrentDisplayMode(0, &display_mode);
    if (status < 0) {
        cout << SDL_GetError() << endl;
        return status;
    }
    SDL_ShowCursor(SDL_DISABLE);
    int const w = display_mode.w;
    int const h = display_mode.h;
    SDL_Window *const window = SDL_CreateWindow("Game", SDL_WINDOWPOS_UNDEFINED, SDL_WINDOWPOS_UNDEFINED, w, h, SDL_WINDOW_FULLSCREEN);
    SDL_Renderer *const renderer = SDL_CreateRenderer(window, -1, SDL_RENDERER_ACCELERATED);
    std::random_device os_seed;
    uint_least32_t const seed = os_seed();
    std::mt19937 generator(seed);
    game_clock clock;
    game g(clock, generator, pair(w, h));

    const long long dt = 10000;
    long long current_time = now_us();
    long long accumulator = 0;

    while (!g.stopped) {
        long long new_time = now_us();
        long long frame_time = new_time - current_time;
        current_time = new_time;

        accumulator += frame_time;

        while (accumulator >= dt) {
            SDL_Event e;
            while (SDL_PollEvent(&e)) {
                if (quit_requested(e)) {
                    g.quit();
                } else if (e.type == SDL_KEYDOWN) {
                    g.handle_keydown(e.key.keysym.sym);
                } else if (e.type == SDL_KEYUP) {
                    g.handle_keyup(e.key.keysym.sym);
                }
            }
            g.tick();
            accumulator -= dt;
        }

        SDL_SetRenderDrawColor(renderer, 0, 0, 0, SDL_ALPHA_OPAQUE);
        SDL_RenderClear(renderer);
        for (space_object bullet: g.bullets) {
            render_rectangle(renderer, bullet.position, bullet.size, color(255, 255, 255));
        }
        for (space_object enemy: g.enemies) {
            render_rectangle(renderer, enemy.position, enemy.size, color(255, 0, 0));
        }
        render_player(renderer, g);
        render_health_bar(renderer, g);
        render_score_bar(renderer, g);
        SDL_RenderPresent(renderer);
    }

    SDL_DestroyWindow(window);
    SDL_Quit();

    return 0;
}
