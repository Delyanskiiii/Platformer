#pragma once
#include "input.h"
#include "dynamic.h"

class Player : public Dynamic {
    private:
        Input inputer = Input();
        Vector2 input;
        // Vector2 accuratePosition = { position.x * 10 , position.y * 10 };
        // Vector2 velocity = { 0, 0 };
        // int runningAcceleration = 5;
        int maxRunningSpeed = 10;
        int slidingDeceleration = 2;
        int jumpSpeed = 50;
        bool grounded = false;

    public:
        const char* state = "idle";
        Vector2 velocity = { 0, 0 };
        Vector2 accuratePosition = { position.x * 10 , position.y * 10 };
        int runningAcceleration = 5;
        using Dynamic::Dynamic;
        void Update();
};