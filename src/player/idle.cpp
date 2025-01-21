#include <iostream>
#include "idle.h"

void Idle::Movement() {
    input = player_->inputer.GetInput();

    if (input.x != 0) {
        player_->TransitionTo(new Run(player_));
    }
    // if ((position.x < screen.x - size.x && position.x > 0) || (position.x == screen.x - size.x && input.x < 0) || (position.x == 0 && input.x > 0)) {
    //     if (direction) {
    //         TransitionTo(new Run(this));
    //         direction = !direction;
    //     }
    //     position.x += input.x * velocity.x;
    // } else if (!direction) {
    //     TransitionTo(new Idle(this));
    //     direction = !direction;
    // }
}