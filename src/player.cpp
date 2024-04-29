#include <raylib.h>
#include "player.h"
#include "input.h"

Player::Player(int width, int height) {
    screen = {width, height};
    // size.x = 10;
    size = { 1, 1 };
    position = { 0, 0 };
    velocity = { 1, 1 };
    inputer = Input();
    // Pair pastVelocity[100];
}

Vector2 Player::Update() {
    input = inputer.GetInput();

    if ((position.x < screen.x - size.x && position.x > 0) || (position.x == screen.x - size.x && input.x < 0) || (position.x == 0 && input.x > 0))
        position.x += input.x * velocity.x;

     if ((position.y < screen.y - size.y && position.y  > 0) || (position.y == screen.y - size.y && input.y < 0) || (position.y  == 0 && input.y > 0))
        position.y += input.y * velocity.y;

    return position;
}

void Player::Draw() {
    DrawRectangle(position.x, position.y, size.x, size.y, WHITE);
}