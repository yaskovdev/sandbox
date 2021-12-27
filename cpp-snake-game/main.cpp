#include <iostream>
#include <unordered_map>
#include "SDL.h"
#include "game.h"
#include "pair.h"
#include "triple.h"

using std::cout;
using std::endl;
using std::make_pair;

void render_square(SDL_Renderer *const renderer, pair const position, pair const size, triple const color) {
    SDL_Rect rectangle;
    rectangle.x = position.x;
    rectangle.y = position.y;
    rectangle.w = size.x;
    rectangle.h = size.y;
    SDL_SetRenderDrawColor(renderer, color.x, color.y, color.z, SDL_ALPHA_OPAQUE);
    SDL_RenderFillRect(renderer, &rectangle);
}

bool visible = true;
unsigned int most_recent_visibility_change = 0;

void render_player(SDL_Renderer *const renderer, game const &game) {
    if (game.collided_recently()) {
        if (game.time - most_recent_visibility_change > 50) {
            visible = !visible;
            most_recent_visibility_change = game.time;
        }
    } else {
        visible = true;
    }
    if (visible) {
        render_square(renderer, game.player.position, game.player.size, triple(255, 255, 255));
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
    game game;

    SDL_Init(SDL_INIT_VIDEO);
    pair field_size = game.field_size;
    SDL_Window *const window = SDL_CreateWindow("Game", SDL_WINDOWPOS_UNDEFINED, SDL_WINDOWPOS_UNDEFINED, field_size.x, field_size.y, 0);
    SDL_Renderer *const renderer = SDL_CreateRenderer(window, -1, SDL_RENDERER_SOFTWARE);

    while (game.ongoing) {
        SDL_Event e;
        while (SDL_PollEvent(&e)) {
            if (quit_requested(e)) {
                game.quit();
            } else if (e.type == SDL_KEYDOWN) {
                game.handle_keydown(e.key.keysym.sym);
            } else if (e.type == SDL_KEYUP) {
                game.handle_keyup(e.key.keysym.sym);
            }
        }
        game.tick();
        SDL_SetRenderDrawColor(renderer, 0, 0, 0, SDL_ALPHA_OPAQUE);
        SDL_RenderClear(renderer);
        for (enemy enemy: game.enemies) {
            render_square(renderer, enemy.position, enemy.size, triple(255, 0, 0));
        }
        render_player(renderer, game);
        render_health(game);
        SDL_RenderPresent(renderer);
    }

    SDL_DestroyWindow(window);
    SDL_Quit();

    return 0;
}
