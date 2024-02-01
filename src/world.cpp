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

    std::ifstream block_file("levels/blocks.txt");

    if (block_file.is_open()) {
        std::string line;
        while (std::getline(block_file, line)) {
            std::istringstream iss(line);
            Block loaded_block;

            for (int i = 0; i < 25; ++i) {
                if (!(iss >> loaded_block.pixels[i].depth >> loaded_block.pixels[i].red >> loaded_block.pixels[i].green >> loaded_block.pixels[i].blue)) {
                    break;
                }
            }

            blocks.push_back(loaded_block);
        }
        block_file.close();
    }

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

void World::Update() {
    Vector2 player_position = player.Update();
}

void World::Draw() {
    for (const auto& block_position : block_positions) {
        auto block = blocks[block_position.block_index];
        // DrawRectangle(0, 0, 1 * resolution_scale, 1 * resolution_scale, Color{current_block.pixels[0].red, current_block.pixels[0].green, current_block.pixels[0].blue, 255});

        for (int i = 0; i < 25; ++i) {
            DrawRectangle(block_position.x * resolution_scale * 5 + i % 5 * resolution_scale, block_position.y * resolution_scale * 5 + i / 5 * resolution_scale, 1 * resolution_scale, 1 * resolution_scale, Color{block.pixels[i].red, block.pixels[i].green, block.pixels[i].blue, 255});
        }
    }
    player.Draw();
}
