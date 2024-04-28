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
    texFull = LoadTexture("levels/shader_test.png");
}

Vector2 World::Update() {
    Vector2 player_position = player.Update();
    return player_position;
}

void World::Draw() {
    DrawTextureEx(texFull, {0, 0}, 0, 1, WHITE);
    // DrawTexture(texFull, );
    // DrawTextureEx(texFull, {0, 0}, 0, resolution_scale, WHITE);
    // for (const auto& block_position : block_positions) {
    //     DrawTextureEx(texFull, {block_position.x * resolution_scale * 5, block_position.y * resolution_scale * 5}, 0, resolution_scale, WHITE);
    // }
    // player.Draw();
}
