#include <iostream>
#include <unordered_map>
#include "SDL.h"
#include "game.h"
#include "pair.h"

using std::cout;
using std::endl;
using std::make_pair;

void render_square(SDL_Renderer *const renderer, pair const position, pair const size) {
    SDL_Rect rectangle;
    rectangle.x = position.x;
    rectangle.y = position.y;
    rectangle.w = size.x;
    rectangle.h = size.y;
    SDL_SetRenderDrawColor(renderer, 255, 255, 255, SDL_ALPHA_OPAQUE);
    SDL_RenderFillRect(renderer, &rectangle);
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
        render_square(renderer, game.player_position, game.player_size);
        render_square(renderer, game.main_enemy.position, game.main_enemy.size);
//        for (enemy enemy: game.enemies) {
//        }
        SDL_RenderPresent(renderer);
    }

    SDL_DestroyWindow(window);
    SDL_Quit();

    return 0;
}
