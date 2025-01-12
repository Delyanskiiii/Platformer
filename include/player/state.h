#pragma once

class Player;

class State {
    protected:
        Player* player_;

    public:
        State(Player* player);
        
        virtual ~State();
        virtual void Movement() = 0;
};