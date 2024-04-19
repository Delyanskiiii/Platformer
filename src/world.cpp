#include <iostream>
#include <raylib.h>
#include <fstream>
#include "world.h"
#include <sstream>
#include <vector>
#include <algorithm>
#include "player.h"

World::World(int diff) {
    resolution_scale = diff;
    player = Player(resolution_scale);
    texFull = LoadTexture("levels/light_test.png");

    // std::ifstream block_file("levels/blocks.txt");

    // if (block_file.is_open()) {
    //     std::string line;
    //     while (std::getline(block_file, line)) {
    //         std::istringstream iss(line);
    //         Block loaded_block;

    //         for (int i = 0; i < 25; ++i) {
    //             if (!(iss >> loaded_block.pixels[i].depth >> loaded_block.pixels[i].red >> loaded_block.pixels[i].green >> loaded_block.pixels[i].blue)) {
    //                 break;
    //             }
    //         }

    //         blocks.push_back(loaded_block);
    //     }
    //     block_file.close();
    // }

    std::ifstream level_file("levels/level_1.txt");

    if (level_file.is_open()) {
        std::string line;
        while (std::getline(level_file, line)) {
            std::istringstream iss(line);
            Position loaded_position;

            if (!(iss >> loaded_position.x >> loaded_position.y >> loaded_position.block_index)) {
                break;
            }

            block_positions.push_back(loaded_position);
        }
        level_file.close();
    }
}

Vector2 World::Update() {
    Vector2 player_position = player.Update();
    return player_position;
}

void World::Draw() {
    DrawTextureEx(texFull, {0, 0}, 0, resolution_scale, WHITE);
    // for (const auto& block_position : block_positions) {
    //     DrawTextureEx(texFull, {block_position.x * resolution_scale * 5, block_position.y * resolution_scale * 5}, 0, resolution_scale, WHITE);
    // }
    player.Draw();
}
