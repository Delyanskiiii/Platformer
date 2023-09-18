#include <raylib.h>
#include "player.h"
#include "pair.h"
// #include "main.h"

extern int diff;

Player::Player() {
    size.Set(5, 5);
    position.Set(20, 20);
    velocity.Set(1, 1);
    // Pair pastVelocity[100];
}

void Player::Update(Pair input) {
    if (position.value1 + size.value1 / 2 < GetScreenWidth() / diff && position.value1 - size.value1 / 2 > 0)
        position.value1 += input.value1 * velocity.value1;

    if (position.value2 + size.value2 / 2 < GetScreenHeight() / diff && position.value2 - size.value2 / 2 > 0)
        position.value2 += input.value2 * velocity.value2;
}

void Player::Draw() {
    DrawRectangle(position.value1 * diff, position.value2 * diff, size.value1 * diff, size.value2 * diff, WHITE);
}