#pragma once
#include <vector>

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
        World();
        // void Update();
        void Draw();
        std::vector<Block> blocks;
    // private:

};