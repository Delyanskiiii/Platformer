#include <raylib.h>
#include "player.h"
#include "input.h"
#include "context.h"
#include "running.h"
#include "constants.h"

Player::Player() {
    screen = {float(TARGET_WIDTH), float(TARGET_HEIGHT)};
    Context *context = new Context(new RunningLeft);
    context->Request1();
    context->Request2();
    delete context;
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