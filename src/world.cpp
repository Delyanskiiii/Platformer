#include <iostream>
#include <raylib.h>
#include <fstream>
#include "world.h"
#include <sstream>
#include <vector>
#include <algorithm>
#include "player.h"

World::World(int width, int height) {
    player = Player(width, height);
    texFull = LoadTexture("texture.png");
}

Vector2 World::Update() {
    Vector2 player_position = player.Update();
    return player_position;
}

void World::Draw() {
    DrawTextureEx(texFull, {0, 0}, 0, 1, WHITE);
    player.Draw();
}
