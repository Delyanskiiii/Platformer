#pragma once
#include "state.h"

class Context {
    private:
        State *state_;

    public:
        Context(State *state);
        ~Context();

        void TransitionTo(State *state);
        void Request1();
        void Request2();
};