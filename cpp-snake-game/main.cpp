#include <iostream>
#include <unordered_map>
#include "SDL.h"
#include "game.h"

using std::cout;
using std::endl;
using std::pair;
using std::make_pair;

void render_square(SDL_Renderer *const renderer, pair<int, int> const position) {
    SDL_Rect rectangle;
    rectangle.x = position.first;
    rectangle.y = position.second;
    rectangle.w = 50;
    rectangle.h = 50;
    SDL_SetRenderDrawColor(renderer, 0, 255, 0, SDL_ALPHA_OPAQUE);
    SDL_RenderFillRect(renderer, &rectangle);
}

int main(int const argc, char const *const argv[]) {
    SDL_Init(SDL_INIT_VIDEO);
    SDL_Window *const window = SDL_CreateWindow("SDL2Test", SDL_WINDOWPOS_UNDEFINED, SDL_WINDOWPOS_UNDEFINED, 640, 480, 0);
    SDL_Renderer *const renderer = SDL_CreateRenderer(window, -1, SDL_RENDERER_SOFTWARE);

    game game;
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
        render_square(renderer, game.position);
        SDL_RenderPresent(renderer);
    }

    SDL_DestroyWindow(window);
    SDL_Quit();

    return 0;
}
