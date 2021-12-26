#ifndef CPP_SANDBOX_ENTITY_H
#define CPP_SANDBOX_ENTITY_H


#include "pair.h"

class entity {
public:
    pair count;

    entity();

    void print_name();

    void increment();

    ~entity();

private:
    char const *name_;
};


#endif //CPP_SANDBOX_ENTITY_H
