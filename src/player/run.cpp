#include "run.h"

void Run::ShouldTransition() {
    if (player_->input.x == 0) {
        player_->TransitionTo(new Idle(player_));
    }
}

void Run::Movement() {
    player_->position.x += player_->input.x * player_->velocity.x;
}