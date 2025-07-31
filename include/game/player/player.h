#pragma once
#include "input.h"
#include "dynamic.h"

class Player : public Dynamic {
    private:
        Input inputer = Input();
        Vector2 input;
        Vector2 velocity = { 1, 1 };

    public:
        using Dynamic::Dynamic;
        void Update();
};