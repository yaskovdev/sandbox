#include <iostream>
#include "entity.h"

entity::entity() {
    name_ = "John Doe";
    std::cout << "Entity created" << std::endl;
}

entity::~entity() {
    std::cout << "Entity deleted" << std::endl;
}

void entity::print_name() const {
    std::cout << "Entity name is " << name_ << std::endl;
}

void entity::increment() {
    count = pair(count.x, count.y + 1);
}
