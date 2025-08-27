#pragma once
#include "static.h"

class Dynamic : public Static {
    protected:
        int Direction(int var1, int var2);
        Image collisionImage;

    public:
        Dynamic(Texture2D texture, int layer, Vector2 position, Image collisionImage);
        virtual void SetPosition(Vector2 position);
        void Translate(Vector2 destination);
        void Update();
        bool Grounded(Vector2 location);
};