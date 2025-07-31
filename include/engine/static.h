#pragma once
#include <raylib.h>
#include <stdexcept>

class Static {
    protected:
        Vector2 position = {0, 0};
        int layer;
        Texture2D texture;
        Image image;

    public:
        Static(Texture2D texture, int layer);
        Vector2 GetPosition();
        int GetLayer();
        Image GetImage();
        void SetLayer(int layer);
        void Draw();
};