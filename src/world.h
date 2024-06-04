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
        World(int width, int height);
        Vector2 Update();
        void Draw();
        // ~World() { delete[] blocks; }

    private:
        std::vector<Block> blocks;
        std::vector<Position> block_positions;
        Texture2D texture;
        Vector2 player_position;
        Player player = Player(1, 1);
};