#pragma once
#include <raylib.h>
#include <stdexcept>
#include "rlImGui.h"
#include "imgui.h"

class Static {
    protected:
        Vector2 position = {0, 0};
        int layer;
        Texture2D texture;
        Image image;

    public:
        Static(Texture2D texture, int layer, Vector2 position = {0, 0});
        Vector2 GetPosition();
        int GetLayer();
        Image GetImage();
        void SetLayer(int layer);
        void Draw();
};