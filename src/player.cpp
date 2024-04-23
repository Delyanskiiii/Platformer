#include <raylib.h>
#include "player.h"
#include "input.h"

Player::Player(int diff, float width, float height) {
    resolution_scale = diff;
    screen = {width, height};
    // size.x = 10;
    size = { 5, 5 };
    position = { 0, 1360 };
    velocity = { 8, 8 };
    inputer = Input();
    // Pair pastVelocity[100];
}

Vector2 Player::Update() {
    input = inputer.GetInput();

    if ((position.x < screen.x - size.x * resolution_scale && position.x > 0) || (position.x == screen.x - size.x * resolution_scale && input.x < 0) || (position.x == 0 && input.x > 0))
        position.x += input.x * velocity.x;

     if ((position.y < screen.y - size.y * resolution_scale && position.y  > 0) || (position.y == screen.y - size.y * resolution_scale && input.y < 0) || (position.y  == 0 && input.y > 0))
        position.y += input.y * velocity.y;

    return position;
}

void Player::Draw() {
    DrawRectangle(position.x, position.y, size.x * resolution_scale, size.y * resolution_scale, WHITE);
}