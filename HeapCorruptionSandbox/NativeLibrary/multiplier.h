#pragma once

class multiplier
{
    int coefficient_;
public:
    explicit multiplier(const int coefficient): coefficient_(coefficient)
    {
    }

    int multiply(int a) const;
};
