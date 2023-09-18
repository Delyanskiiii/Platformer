#pragma once
#include "pair.h"

class Player
{
    public:
        Player();
        void Update(Pair input);
        void Draw();

    private:
        Pair size;
        Pair position;
        Pair velocity;
        Pair pastVelocity[100];
};