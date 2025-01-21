#pragma once
#include "state.h"
#include "idle.h"
#include "glide.h"
#include "slide.h"

class Run : public State {
    public:
        using State::State;
        void Movement() override;
};