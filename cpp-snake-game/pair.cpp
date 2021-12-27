#include "pair.h"

pair::pair() : pair(0, 0) {}

pair::pair(int x, int y) : x(x), y(y) {}

bool pair::intersects(pair p) const {
    return p.x <= y && x <= p.y;
}
