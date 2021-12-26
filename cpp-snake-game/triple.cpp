//
// Created by Sergey Yaskov on 26.12.2021.
//

#include "triple.h"

triple::triple() : triple(0, 0, 0) {

}

triple::triple(int x, int y, int z) {
    this->x = x;
    this->y = y;
    this->z = z;
}
