#pragma once
#include "static.h"

class Dynamic : public Static {
    protected:
        int Direction(int var1, int var2);
        Vector2 accuratePosition = { position.x * 10 , position.y * 10 };
        Image collisionImage;

    public:
        Dynamic(Texture2D texture, int layer, Vector2 position, Image collisionImage);
        virtual void SetPosition(Vector2 position);
        void SetAccuratePosition(Vector2 position);
        void Translate(Vector2 accurateDestination);
        void Update();
        int HorizontallyGrounded(Vector2 location);
        int VerticallyGrounded(Vector2 location);
};