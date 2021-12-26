#include <iostream>
#include "SDL.h"

#define SPEED 10

using std::cout;
using std::endl;

void render_square(std::pair<int, int> const position, SDL_Renderer *renderer) {
    SDL_Rect rectangle;
    rectangle.x = position.first;
    rectangle.y = position.second;
    rectangle.w = 50;
    rectangle.h = 50;
    SDL_SetRenderDrawColor(renderer, 0, 255, 0, SDL_ALPHA_OPAQUE);
    SDL_RenderFillRect(renderer, &rectangle);
}

void update(std::pair<int, int> *const position, std::pair<int, int> const update) {
    position->first += update.first;
    position->second += update.second;
}

int main(int argc, char *argv[]) {
    SDL_Init(SDL_INIT_VIDEO);

    SDL_Window *window = SDL_CreateWindow("SDL2Test", SDL_WINDOWPOS_UNDEFINED, SDL_WINDOWPOS_UNDEFINED, 640, 480, 0);

    SDL_Renderer *renderer = SDL_CreateRenderer(window, -1, SDL_RENDERER_SOFTWARE);

    SDL_Event e;
    bool quit = false;
    std::pair<int, int> position = std::make_pair(0, 0);
    int x = 0;
    int y = 0;
    while (!quit) {
        while (SDL_PollEvent(&e)) {
            if (e.type == SDL_QUIT) {
                quit = true;
            } else if (e.type == SDL_KEYDOWN) {
                std::pair<int, int> delta;
                switch (e.key.keysym.sym) {
                    case SDLK_UP:
                        delta = std::make_pair(0, -SPEED);
                        break;
                    case SDLK_DOWN:
                        delta = std::make_pair(0, SPEED);
                        break;
                    case SDLK_RIGHT:
                        delta = std::make_pair(SPEED, 0);
                        break;
                    case SDLK_LEFT:
                        delta = std::make_pair(-SPEED, 0);
                        break;
                }
                update(&position, delta);
            }
        }
        SDL_SetRenderDrawColor(renderer, 0, 0, 0, SDL_ALPHA_OPAQUE);
        SDL_RenderClear(renderer);
        render_square(position, renderer);
        SDL_RenderPresent(renderer);
    }

    SDL_DestroyWindow(window);
    SDL_Quit();

    return 0;
}
