#pragma once
#include <raylib.h>
#include "player.h"

class World {
    private:
        Texture2D texture;
        Vector2 player_position;
        Player player = Player(this);
        Image image;

    public:
        World();

        Vector2 Update();
        void Draw();

        Vector2 NewPosition(Vector2 origin, Vector2 destination);
};