#include <iostream>
#include <unordered_map>
#include "SDL.h"

#define SPEED 20

using std::cout;
using std::endl;
using std::pair;
using std::make_pair;

std::unordered_map<SDL_KeyCode, pair<int, int>> key_to_delta({
    {SDLK_UP,    make_pair(0, -SPEED)},
    {SDLK_DOWN,  make_pair(0, SPEED)},
    {SDLK_LEFT,  make_pair(-SPEED, 0)},
    {SDLK_RIGHT, make_pair(SPEED, 0)}
});

void render_square(SDL_Renderer *const renderer, pair<int, int> const position) {
    SDL_Rect rectangle;
    rectangle.x = position.first;
    rectangle.y = position.second;
    rectangle.w = 50;
    rectangle.h = 50;
    SDL_SetRenderDrawColor(renderer, 0, 255, 0, SDL_ALPHA_OPAQUE);
    SDL_RenderFillRect(renderer, &rectangle);
}

void update(pair<int, int> *const position, pair<int, int> const delta) {
    position->first += delta.first;
    position->second += delta.second;
}

int main(int const argc, char const *const argv[]) {
    SDL_Init(SDL_INIT_VIDEO);
    SDL_Window *const window = SDL_CreateWindow("SDL2Test", SDL_WINDOWPOS_UNDEFINED, SDL_WINDOWPOS_UNDEFINED, 640, 480, 0);
    SDL_Renderer *const renderer = SDL_CreateRenderer(window, -1, SDL_RENDERER_SOFTWARE);

    bool quit = false;
    pair<int, int> position = make_pair(0, 0);
    while (!quit) {
        SDL_Event e;
        while (SDL_PollEvent(&e)) {
            if (e.type == SDL_QUIT) {
                quit = true;
            } else if (e.type == SDL_KEYDOWN) {
                update(&position, key_to_delta[(SDL_KeyCode) e.key.keysym.sym]);
            }
        }
        SDL_SetRenderDrawColor(renderer, 0, 0, 0, SDL_ALPHA_OPAQUE);
        SDL_RenderClear(renderer);
        render_square(renderer, position);
        SDL_RenderPresent(renderer);
    }

    SDL_DestroyWindow(window);
    SDL_Quit();

    return 0;
}
