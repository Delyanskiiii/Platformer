#pragma once
#include "input.h"
#include "dynamic.h"

class Player : public Dynamic {
    private:
        Vector2 velocity = { 0, 0 };
        int runningAcceleration = 5;
        int maxRunningSpeed = 10;
        int slidingDeceleration = 2;
        int minSlidingThreshold = 6;
        int jumpSpeed = 50;
        int horizontallyGrounded = 0;
        int verticallyGrounded = 0;

    public:
        typedef enum State { IDLE = 0, RUN, SLIDE, GLIDE } State;
        State currentState = IDLE;
        using Dynamic::Dynamic;
        void Update(Vector2 input);
        void Debug();
};