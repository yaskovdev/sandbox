#include <iostream>
#include <unordered_map>
#include "SDL.h"
#include "game.h"

using std::cout;
using std::endl;
using std::pair;
using std::make_pair;

void render_square(SDL_Renderer *const renderer, pair<int, int> const position, pair<int, int> const size) {
    SDL_Rect rectangle;
    rectangle.x = position.first;
    rectangle.y = position.second;
    rectangle.w = size.first;
    rectangle.h = size.second;
    SDL_SetRenderDrawColor(renderer, 0, 255, 0, SDL_ALPHA_OPAQUE);
    SDL_RenderFillRect(renderer, &rectangle);
}

int main(int const argc, char const *const argv[]) {
    game game;

    SDL_Init(SDL_INIT_VIDEO);
    pair<int, int> field_size = game.field_size;
    SDL_Window *const window = SDL_CreateWindow("Game", SDL_WINDOWPOS_UNDEFINED, SDL_WINDOWPOS_UNDEFINED, field_size.first, field_size.second, 0);
    SDL_Renderer *const renderer = SDL_CreateRenderer(window, -1, SDL_RENDERER_SOFTWARE);

    while (game.ongoing) {
        SDL_Event e;
        while (SDL_PollEvent(&e)) {
            if (e.type == SDL_QUIT) {
                game.quit();
            } else if (e.type == SDL_KEYDOWN) {
                game.handle_keydown(e.key.keysym.sym);
            }
        }
        SDL_SetRenderDrawColor(renderer, 0, 0, 0, SDL_ALPHA_OPAQUE);
        SDL_RenderClear(renderer);
        render_square(renderer, game.player_position, game.player_size);
        SDL_RenderPresent(renderer);
    }

    SDL_DestroyWindow(window);
    SDL_Quit();

    return 0;
}
