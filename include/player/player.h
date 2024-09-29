#pragma once
#include "input.h"
#include <raylib.h>

class Player
{
    public:
        Player();
        Vector2 Update();
        void Draw();
        // Vector2 position() { return position; }

    private:
        int resolution_scale;
        Input inputer = Input();
        Vector2 size = { 1, 1 };
        Vector2 screen;
        Vector2 input;
        Vector2 position = { 0, 0 };
        Vector2 velocity = { 1, 1 };
        Vector2 pastVelocity[100];
};