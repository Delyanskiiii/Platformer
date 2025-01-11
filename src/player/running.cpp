#include <iostream>
#include "running.h"
#include "player.h"


void RunningLeft::Handle1() {
    std::cout << "Moving left.\n";
}

// void RunningLeft::Handle2() {
//     std::cout << "Change in directions.\n";
//     this->player_->TransitionTo(new RunningRight);
// }


void RunningRight::Handle1() {
    std::cout << "Moving right.\n";
}

// void RunningRight::Handle2() {
//     std::cout << "Change in directions.\n";
//     this->player_->TransitionTo(new RunningLeft);
// }