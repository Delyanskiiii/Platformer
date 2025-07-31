#include "dynamic.h"
#include <cmath>
#include <iostream>

Dynamic::Dynamic(Texture2D texture, int layer, Image collisionImage) : Static(texture, layer) {
    this->collisionImage = collisionImage;
}

void Dynamic::SetPosition(Vector2 position) {
    this->position = position;
}

int Dynamic::Direction(int var1, int var2) {
    if (var1 > var2) {
        return 1;
    } else if (var1 < var2) {
        return -1;
    } else {
        return 0;
    }
}

void Dynamic::Translate(Vector2 destination) {
    Vector2 variance = {float(Direction(destination.x, this->position.x)), float(Direction(destination.y, this->position.y))};
    float a = float(destination.y - this->position.y) / (destination.x - this->position.x);
    float b = destination.y - a * destination.x;
    Vector2 currentPixelLocation = this->position;
    Vector2 originalPoint;

    while (currentPixelLocation.x != destination.x || currentPixelLocation.y != destination.y) {
        originalPoint = currentPixelLocation;

        if (abs(destination.x - this->position.x) < abs(destination.y - this->position.y)) {
            if (currentPixelLocation.y == destination.y || floor((currentPixelLocation.x + variance.x) * a + b + 0.5) == currentPixelLocation.y) {
                currentPixelLocation.x += variance.x;
            } else if (currentPixelLocation.y != destination.y) {
                currentPixelLocation.y += variance.y;
            } else {
                SetPosition(currentPixelLocation);
                return;
            }
        } else {
            if ((currentPixelLocation.x == destination.x || floor((currentPixelLocation.y + variance.y - b) / a + 0.5) == currentPixelLocation.x) && (int)GetImageColor(this->collisionImage, currentPixelLocation.x, currentPixelLocation.y + variance.y).a == 0) {
                currentPixelLocation.y += variance.y;
            } else if (currentPixelLocation.x != destination.x && (int)GetImageColor(this->collisionImage, currentPixelLocation.x + variance.x, currentPixelLocation.y).a == 0) {
                currentPixelLocation.x += variance.x;
            } else if (currentPixelLocation.y != destination.y && (int)GetImageColor(this->collisionImage, currentPixelLocation.x, currentPixelLocation.y + variance.y).a == 0) {
                currentPixelLocation.y += variance.y;
            } else {
                SetPosition(currentPixelLocation);
                return;
            }
        }
    }

    SetPosition(destination);
    return;
}

void Dynamic::Update() {

}