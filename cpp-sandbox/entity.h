#ifndef CPP_SANDBOX_ENTITY_H
#define CPP_SANDBOX_ENTITY_H


class entity {
public:
    entity();

    void print_name();

    ~entity();

private:
    char const *name_;
};


#endif //CPP_SANDBOX_ENTITY_H
