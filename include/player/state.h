#pragma once

#include "player.h"

class State {
    protected:
        Player* player_;

    public:
        State(Player* player);
        
        virtual ~State();
        // virtual void ShouldExit() = 0;
        virtual void Movement();
};