#include "color.h"

color::color() : color(0, 0, 0) {

}

color::color(int r, int g, int b) {
    this->r = r;
    this->g = g;
    this->b = b;
}
