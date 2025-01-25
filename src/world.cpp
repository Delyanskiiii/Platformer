#include <raylib.h>
#include "world.h"
#include "player.h"

World::World() {
    texture = LoadTexture("textures/Level.png");
    image = LoadImageFromTexture(texture);
}

Vector2 World::Update() {
    player_position = player.Update();
    return player_position;
}

void World::Draw() {
    DrawTextureV(texture, {0, 0}, WHITE);
    player.Draw();
}

Vector2 World::NewPosition(Vector2 origin, Vector2 destination) {
    if (GetImageColor(image, destination.x, destination.y).a != 0) {
        return origin;
    }
    return {destination.x, destination.y};
}
