#include <iostream>
#include "running.h"
#include "context.h"


void RunningLeft::Handle1() {
    std::cout << "Moving left.\n";
}

void RunningLeft::Handle2() {
    std::cout << "Change in directions.\n";
    this->context_->TransitionTo(new RunningRight);
}


void RunningRight::Handle1() {
    std::cout << "Moving right.\n";
}

void RunningRight::Handle2() {
    std::cout << "Change in directions.\n";
    this->context_->TransitionTo(new RunningLeft);
}