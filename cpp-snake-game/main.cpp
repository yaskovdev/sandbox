#include <iostream>
#include <unordered_map>
#include "SDL.h"
#include "game.h"
#include "pair.h"
#include "color.h"

using std::cout;
using std::endl;
using std::make_pair;

void render_square(SDL_Renderer *const renderer, pair const position, pair const size, color const color) {
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
    if (game.player.collided_recently(game.time)) {
        if (game.time - most_recent_visibility_change > 50) {
            visible = !visible;
            most_recent_visibility_change = game.time;
        }
    } else {
        visible = true;
    }
    if (visible) {
        render_square(renderer, game.player.position, game.player.size, color(255, 255, 255));
    }
};

void render_health(game const &game) {
    if (game.time % 500 == 0) {
        cout << std::to_string(game.player.health) << endl;
        cout << std::to_string(game.player.most_recent_collision_time) << endl;
    }
}

bool quit_requested(const SDL_Event e) {
    return e.type == SDL_QUIT || (e.type == SDL_KEYDOWN && e.key.keysym.sym == SDLK_ESCAPE);
}

int main(int const argc, char const *const argv[]) {
    game g;
    SDL_Init(SDL_INIT_VIDEO);
    pair field_size = g.field_size;
    SDL_Window *const window = SDL_CreateWindow("Game", SDL_WINDOWPOS_UNDEFINED, SDL_WINDOWPOS_UNDEFINED, field_size.x, field_size.y, 0);
    SDL_Renderer *const renderer = SDL_CreateRenderer(window, -1, SDL_RENDERER_SOFTWARE);

    while (g.ongoing) {
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
        SDL_SetRenderDrawColor(renderer, 0, 0, 0, SDL_ALPHA_OPAQUE);
        SDL_RenderClear(renderer);
        for (enemy enemy: g.enemies) {
            render_square(renderer, enemy.position, enemy.size, color(255, 0, 0));
        }
        render_player(renderer, g);
        render_health(g);
        SDL_RenderPresent(renderer);
    }

    SDL_DestroyWindow(window);
    SDL_Quit();

    return 0;
}
