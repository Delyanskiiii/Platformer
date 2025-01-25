#pragma once

#include <raylib.h>
#include "input.h"

class State;
class World;

class Player {
    private:
        State* state_;
        Input inputer = Input();
        World* world_;

    public:
        Player(World* world);
        ~Player();

        Vector2 Update();
        void Draw();
        
        void TransitionTo(State* state);
        Vector2 NewPosition(Vector2 origin, Vector2 destination);
        
        Vector2 input;
        Vector2 position = { 160, 90 };
        Vector2 velocity = { 1, 1 };
};