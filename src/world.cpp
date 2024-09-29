#include <iostream>
#include <raylib.h>
#include <fstream>
#include "world.h"
#include <sstream>
#include <vector>
#include <algorithm>
#include "player.h"

World::World() {
    texture = LoadTexture("textures/texture.png");
}

Vector2 World::Update() {
    player_position = player.Update();
    return player_position;
}

void World::Draw() {
    DrawTextureV(texture, {0, 0}, WHITE);
    player.Draw();
}
