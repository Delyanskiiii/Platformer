#include "state.h"

State::State(Player* player) {
    this->player_ = player;
}

State::~State() {
    // Virtual destructor can be empty or have cleanup code if needed
}

void State::Movement() {
    // ShouldExit();
}