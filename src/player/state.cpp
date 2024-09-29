#include "state.h"

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
void State::set_context(Context* context) {
    this->context_ = context;
}