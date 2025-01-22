#pragma once
#include "state.h"
#include "run.h"
#include "glide.h"
#include "slide.h"

class Idle : public State {
    public:
        using State::State;
        void ShouldTransition() override;
};