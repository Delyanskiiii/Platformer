#pragma once
#include "state.h"
#include "player.h"

class Idle : public State {
    private:
        Vector2 input;

    public:
        using State::State;
        void Movement() override;
};