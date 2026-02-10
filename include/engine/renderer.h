#pragma once
#include "static.h"
#include "dynamic.h"
#include "player.h"

class Renderer {
    private:
        Static backGround = Static(LoadTexture("textures/texture.png"), 0);
        Static middleGround = Static(LoadTexture("textures/Level.png"), 1);
        Player player = Player(LoadTexture("textures/playerTexture.png"), 1, {100, 10}, middleGround.GetImage());
        Input inputer = Input();
        Vector2 input;

    public:
        Renderer();
        Vector2 Update();
        void Draw();
        void Debug();
};