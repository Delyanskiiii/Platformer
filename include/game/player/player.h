#pragma once
#include "input.h"
#include "dynamic.h"

class Player : public Dynamic {
    private:
        Input inputer = Input();
        Vector2 input;
        Vector2 velocity = { 0, 0 };
        int runningAcceleration = 5;
        int maxRunningSpeed = 10;
        int slidingDeceleration = 2;
        int jumpSpeed = 50;
        bool grounded = false;

    public:
        typedef enum State { IDLE = 0, RUN, SLIDE, GLIDE } State;
        State currentState = IDLE;
        using Dynamic::Dynamic;
        void Update();
        void Debug();
};