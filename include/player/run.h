#pragma once
#include "state.h"

class Run : public State {
    public:
        using State::State;
        void Movement() override;
};