#include <raylib.h>
#include "player.h"
#include "idle.h"
#include "state.h"
#include "world.h"

Player::Player(World* world) {
    this->state_ = new Idle(this);
    world_ = world;
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
    DrawRectangle(position.x, position.y, 1, 1, WHITE);
}

Vector2 Player::NewPosition(Vector2 origin, Vector2 destination) {
    return this->world_->NewPosition(origin, destination);
}