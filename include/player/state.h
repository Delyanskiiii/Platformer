#pragma once

class Context;

/**
 * The base State class declares methods that all Concrete State should
 * implement and also provides a backreference to the Context object, associated
 * with the State. This backreference can be used by States to transition the
 * Context to another State.
 */
class State {
protected:
    /**
     * @var Context
     */
    Context* context_;

public:
    virtual ~State();

    /**
     * Sets the context for the state.
     *
     * @param context Pointer to the Context object.
     */
    void set_context(Context* context);

    /**
     * Pure virtual methods to be implemented by Concrete States.
     */
    virtual void Handle1() = 0;
    virtual void Handle2() = 0;
};