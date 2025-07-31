#pragma once
#include "static.h"
#include "dynamic.h"
#include "player.h"

class Renderer {
    private:
        Static backGround = Static(LoadTexture("textures/texture.png"), 0);
        Static middleGround = Static(LoadTexture("textures/Level.png"), 1);
        Player player = Player(LoadTexture("textures/playerTexture.png"), 1, middleGround.GetImage());

    public:
        Renderer();
        Vector2 Update();
        void Draw();
};