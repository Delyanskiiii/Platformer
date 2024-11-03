#pragma once
#include "state.h"

class RunningLeft : public State {
    public:
        void Handle1() override;
        void Handle2() override;
};

class RunningRight : public State {
    public:
        void Handle1() override;
        void Handle2() override;
};