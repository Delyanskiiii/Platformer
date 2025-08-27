#include "static.h"

Static::Static(Texture2D texture, int layer, Vector2 position) {
    this->texture = texture;
    this->position = position;
    this->image = LoadImageFromTexture(texture);
    SetLayer(layer);
}

Vector2 Static::GetPosition() {
    return this->position;
}

int Static::GetLayer() {
    return this->layer;
}

Image Static::GetImage() {
    return this->image;
}

void Static::SetLayer(int layer) {
    if (layer == -1 || layer == 0 || layer == 1) {
        this->layer = layer;
    } else {
        throw std::invalid_argument("Layer must be -1, 0, or 1");
    }
}

void Static::Draw() {
    DrawTextureV(this->texture, this->position, WHITE);
}