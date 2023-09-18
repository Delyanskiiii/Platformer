#pragma once
#include "pair.h"

class Input
{
    public:
        Input();
        Pair GetInput(bool Left, bool Right, bool Up, bool Down);

    private:
        Pair prevInput;
        Pair input;
};