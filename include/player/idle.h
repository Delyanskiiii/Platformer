#pragma once
#include "state.h"
#include "run.h"
#include "glide.h"
#include "slide.h"

class Idle : public State {
    private:
        Vector2 input;

    public:
        using State::State;
        void Movement() override;
};