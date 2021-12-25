#include <iostream>
#include "entity.h"

entity::entity() {
    std::cout << "Entity created" << std::endl;
}

entity::~entity() {
    std::cout << "Entity deleted" << std::endl;
}

void entity::print() {
    std::cout << "Entity print called" << std::endl;
}
