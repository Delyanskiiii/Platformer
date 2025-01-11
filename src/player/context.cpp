// #include "context.h"

// /**
//  * Constructor that initializes the Context with a given State.
//  * It immediately transitions to the provided State.
//  *
//  * @param state Pointer to the initial State.
//  */
// Context::Context(State* state) : state_(nullptr) {
//     this->TransitionTo(state);
// }

// /**
//  * Destructor that deletes the current State to free resources.
//  */
// Context::~Context() {
//     delete state_;
// }

// /**
//  * Allows changing the State object at runtime.
//  * It deletes the old State and sets the new one, updating the backreference.
//  *
//  * @param state Pointer to the new State.
//  */
// void Context::TransitionTo(State* state) {
//     std::cout << "Context: Transition to " << typeid(*state).name() << ".\n";
//     if (this->state_ != nullptr) {
//         delete this->state_;
//     }
//     this->state_ = state;
//     this->state_->set_context(this);
// }

// /**
//  * Delegates the Handle1 action to the current State.
//  */
// void Context::Request1() {
//     std::cout << "Handle 1 ";
//     this->state_->Handle1();
// }

// /**
//  * Delegates the Handle2 action to the current State.
//  */
// void Context::Request2() {
//     this->state_->Handle2();
// }