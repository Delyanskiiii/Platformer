#pragma once
#include <raylib.h>
#include <vector>
#include "player.h"

struct Pixel {
    int depth;
    int red;
    int green;
    int blue;
};

struct Block {
    Pixel pixels[25];
};

struct Position {
    int x;
    int y;
    int block_index;
};

class World {
    public:
        World(int diff);
        void Update();
        void Draw();
        // ~World() { delete[] blocks; }

    private:
        std::vector<Block> blocks;
        std::vector<Position> block_positions;
        int resolution_scale;
        Player player = Player(1);
};