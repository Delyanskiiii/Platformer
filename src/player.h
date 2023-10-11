#pragma once
#include "input.h"
#include <raylib.h>

class Player
{
    public:
        Player(int diff);
        Vector2 Update();
        void Draw();
        // Vector2 position() { return position; }

    private:
        int resolution_scale;
        Input inputer;
        Vector2 size;
        Vector2 input;
        Vector2 position;
        Vector2 velocity;
        Vector2 pastVelocity[100];
};