#include "player.h"

void Player::Update() {
    input = inputer.GetInput();
    Translate({this->position.x + input.x * velocity.x, this->position.y + input.y * velocity.y});
}