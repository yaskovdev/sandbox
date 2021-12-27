#include <iostream>
#include "SDL.h"
#include "game.h"
#include "pair.h"
#include "color.h"

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
        if (game.time() - most_recent_visibility_change > 50) {
            visible = !visible;
            most_recent_visibility_change = game.time();
        }
    } else {
        visible = true;
    }
    if (visible) {
        render_rectangle(renderer, game.player_.position, game.player_.size, color(255, 255, 255));
    }
};

void render_health(SDL_Renderer *const renderer, game const &game) {
    render_rectangle(renderer, pair(10, 10), pair(game.player_.health, 10), color(255, 0, 255));
}

bool quit_requested(const SDL_Event e) {
    return e.type == SDL_QUIT || (e.type == SDL_KEYDOWN && e.key.keysym.sym == SDLK_ESCAPE);
}

int main(int const argc, char const *const argv[]) {
    class clock c;
    game g(c);
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
        for (space_object bullet: g.bullets) {
            render_rectangle(renderer, bullet.position, bullet.size, color(255, 255, 255));
        }
        for (space_object enemy: g.enemies) {
            render_rectangle(renderer, enemy.position, enemy.size, color(255, 0, 0));
        }
        render_player(renderer, g);
        render_health(renderer, g);
        SDL_RenderPresent(renderer);
    }

    SDL_DestroyWindow(window);
    SDL_Quit();

    return 0;
}
