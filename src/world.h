#pragma once
#include <raylib.h>
#include <vector>
#include "player.h"

struct Pixel {
    int red;
    int green;
    int blue;
    int level;
};

struct Block {
    int x;
    int y;
    Pixel pixels[100];
};

class World
{
    public:
        World(int diff);
        void Update();
        void Draw();
        // ~World() { delete[] blocks; }

    private:
        std::vector<Block> blocks;
        std::vector<Block> dynamic_blocks;
        // Block* blocks;
        int resolution_scale;
        Player player = Player(1);
};