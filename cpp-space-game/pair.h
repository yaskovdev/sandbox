#ifndef CPP_SPACE_GAME_PAIR_H
#define CPP_SPACE_GAME_PAIR_H

class pair {
public:
    pair();

    pair(int x, int y);

    int x;
    int y;

    bool intersects(pair p) const;
};

#endif //CPP_SPACE_GAME_PAIR_H
