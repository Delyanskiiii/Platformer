#include <raylib.h>
#include "player.h"
#include "pair.h"

Player::Player()
{
    // Pair size = Pair(10, 10);
    // Pair position = Pair(100, 100);
    // Pair velocity = Pair(0, 0);
    // Pair input = Pair(0, 0);
    // Pair pastVelocity[100];
}

void Player::Update()
{
    position.value1 += velocity.value1;
    position.value2 += velocity.value2;

    if (position.value1 + size.value1 / 2 >= GetScreenWidth() || position.value1 - size.value1 / 2 <= GetScreenWidth())
        velocity.value1 *= -1;

    if (position.value2 + size.value2 / 2 >= GetScreenHeight() || position.value2 - size.value2 / 2 <= GetScreenHeight())
        velocity.value2 *= -1;
}

void Player::Draw()
{
    DrawRectangle(position.value1, position.value2, size.value1, size.value2, WHITE);
}