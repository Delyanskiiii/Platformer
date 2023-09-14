#pragma once
#include "pair.h"

class Player
{
    public:
        Player();
        void Update();
        void Draw();

    private:
        Pair size = Pair(10, 10);
        Pair position = Pair(100, 100);
        Pair velocity = Pair(10, 10);
        Pair input = Pair(0, 0);
        // Pair size;
        // Pair position;
        // Pair velocity;
        // Pair input;
        //  = Pair()
        // Pair pastVelocity[100];
};