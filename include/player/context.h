#include "state.h"
#include <iostream>
#include <typeinfo>

/**
 * The Context defines the interface of interest to clients. It also maintains a
 * reference to an instance of a State subclass, which represents the current
 * state of the Context.
 */
class Context {
private:
    /**
     * @var State A reference to the current state of the Context.
     */
    State* state_;

public:
    /**
     * Constructor that initializes the Context with a given State.
     *
     * @param state Pointer to the initial State.
     */
    Context(State* state);

    /**
     * Destructor that cleans up the current State.
     */
    ~Context();

    /**
     * Allows changing the State object at runtime.
     *
     * @param state Pointer to the new State.
     */
    void TransitionTo(State* state);

    /**
     * Delegates part of its behavior to the current State object.
     */
    void Request1();
    void Request2();
};