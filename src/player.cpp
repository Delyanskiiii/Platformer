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

Vector2 Player::Update() {
    input = inputer.GetInput();

    if ((position.x + size.x < GetScreenWidth() && position.x > 0) || (position.x + size.x == GetScreenWidth() && input.x < 0) || (position.x == 0 && input.x > 0))
        position.x += input.x * velocity.x;

     if ((position.y + size.y < GetScreenHeight() && position.y  > 0) || (position.y + size.y == GetScreenHeight() && input.y < 0) || (position.y  == 0 && input.y > 0))
        position.y += input.y * velocity.y;

    return position;
}

void Player::Draw() {
    DrawRectangle(position.x, position.y, size.x * resolution_scale, size.y * resolution_scale, WHITE);
}