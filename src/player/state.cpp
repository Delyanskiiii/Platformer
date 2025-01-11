#include "state.h"

State::State(Player* player) {
    this->player_ = player;
}

/**
 * Destructor for State. Defined as virtual to ensure proper cleanup of derived classes.
 */
State::~State() {
    // Virtual destructor can be empty or have cleanup code if needed
}

/**
 * Sets the context for the current state.
 *
 * @param context Pointer to the Context object.
 */