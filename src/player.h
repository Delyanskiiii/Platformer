#pragma once
#include "pair.h"

class Player
{
    public:
        Player();
        void Update();
        void Draw();

    private:
        Pair size;
        Pair position;
        Pair velocity;
        Pair input;
        Pair pastVelocity[100];
};