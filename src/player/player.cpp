#include <raylib.h>
#include "player.h"
#include "input.h"
#include "run.h"
#include "idle.h"
#include "constants.h"

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
    this->state_->Movement();
    return position;
}

void Player::Draw() {
    DrawRectangle(position.x, position.y, size.x, size.y, WHITE);
}