#pragma once

#include <raylib.h>
#include "input.h"
#include "constants.h"

class State;

class Player
{
    private:
        int resolution_scale;
        State* state_;
        Vector2 size = { 1, 1 };
        Vector2 screen;
        Vector2 input;
        Vector2 position = { 0, 0 };
        Vector2 velocity = { 1, 1 };
        // Vector2 pastVelocity[100];

    public:
        Input inputer = Input();
        Player();
        Vector2 Update();
        void Draw();
        ~Player();
        void TransitionTo(State* state);
        bool direction = true;
        // Vector2 position() { return position; }
};