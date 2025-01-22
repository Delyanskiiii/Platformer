#include <raylib.h>
#include "player.h"
#include "idle.h"
#include "state.h"

Player::Player() {
    this->state_ = new Idle(this);
    screen = {float(TARGET_WIDTH), float(TARGET_HEIGHT)};
}

Player::~Player() {
    delete state_;
}

void Player::TransitionTo(State* state) {
    delete this->state_;
    this->state_ = state;
}

Vector2 Player::Update() {
    input = inputer.GetInput();
    this->state_->ShouldTransition();
    this->state_->Movement();
    return position;
}

void Player::Draw() {
    DrawRectangle(position.x, position.y, size.x, size.y, WHITE);
}