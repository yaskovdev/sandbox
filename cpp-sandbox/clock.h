#ifndef CPP_SANDBOX_CLOCK_H
#define CPP_SANDBOX_CLOCK_H


class clock {
public:
    clock();

    void tick();

    unsigned int get_time() const;

private:
    unsigned int _time;
};


#endif //CPP_SANDBOX_CLOCK_H
