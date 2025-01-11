#include <raylib.h>
#include "player.h"
#include "input.h"
#include "running.h"
#include "constants.h"

Player::Player() {
    this->state_ = new RunningRight(this);
    screen = {float(TARGET_WIDTH), float(TARGET_HEIGHT)};
}

Player::~Player() {
    delete state_;
}

void Player::TransitionTo(State* state) {
    std::cout << "Context: Transition to " << typeid(*state).name() << ".\n";
    delete this->state_;
    this->state_ = state;
}

Vector2 Player::Update() {
    input = inputer.GetInput();

    if ((position.x < screen.x - size.x && position.x > 0) || (position.x == screen.x - size.x && input.x < 0) || (position.x == 0 && input.x > 0)) {
        if (direction && input.x < 0) {
            TransitionTo(new RunningLeft(this));
            direction = !direction;
        } else if (!direction && input.x > 0) {
            TransitionTo(new RunningRight(this));
            direction = !direction;
        }
        position.x += input.x * velocity.x;
        this->state_->Handle1();
    }

     if ((position.y < screen.y - size.y && position.y  > 0) || (position.y == screen.y - size.y && input.y < 0) || (position.y  == 0 && input.y > 0))
        position.y += input.y * velocity.y;

    return position;
}

void Player::Draw() {
    DrawRectangle(position.x, position.y, size.x, size.y, WHITE);
}