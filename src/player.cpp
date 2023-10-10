#include <raylib.h>
#include "player.h"
#include "input.h"

Player::Player(int diff) {
    resolution_scale = diff;
    size.x = 10;
    size = { 10, 10 };
    position = { 155, 85 };
    velocity = { 1, 1 };
    inputer = Input();
    // Pair pastVelocity[100];
}

void Player::Update() {
    input = inputer.GetInput();

    if ((position.x + size.x < GetScreenWidth() / resolution_scale && position.x > 0) || (position.x + size.x == GetScreenWidth() / resolution_scale && input.x < 0) || (position.x == 0 && input.x > 0))
        position.x += input.x * velocity.x;

     if ((position.y + size.y < GetScreenHeight() / resolution_scale && position.y  > 0) || (position.y + size.y == GetScreenHeight() / resolution_scale && input.y < 0) || (position.y  == 0 && input.y > 0))
        position.y += input.y * velocity.y;
}

void Player::Draw() {
    DrawRectangle(position.x * resolution_scale, position.y * resolution_scale, size.x * resolution_scale, size.y * resolution_scale, WHITE);
}