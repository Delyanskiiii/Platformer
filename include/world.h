#pragma once
#include <raylib.h>
#include <vector>
#include "player.h"

class World {
    public:
        World();
        Vector2 Update();
        void Draw();
        // ~World() { delete[] blocks; }

    private:
        Texture2D texture;
        Vector2 player_position;
        Player player = Player();
};