#include <raylib.h>
#include <cmath>
#include "world.h"
#include "player.h"

World::World() {
    texture = LoadTexture("textures/texture.png");
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
    return destination;
    // float a = destination.x - origin.x;
    // float b = destination.y - origin.y;
    
    // int x = a / abs(a);
    // int y = b / abs(b);

    // // a = a / b;
    // // b = origin.y - a * origin.x;
    
    // while (origin.x != destination.x && origin.y != destination.y) {
    //     if (abs(a) == abs(b)) {
    //         origin.x += x;
    //         origin.y += y;

    //         if (GetImageColor(image, origin.x, origin.y).a > 0) {
    //             origin.y -= y;
    //         }
    //     }



    //     if (floor((origin.x + x) * a + b) == origin.y + y && origin.x != destination.x && origin.y != destination.y) {
    //         origin.x += x;
    //         origin.y += y;

    //         if (GetImageColor(image, origin.x, origin.y).a > 0) {
    //             origin.y -= y;
    //         }

    //         break;
    //     } else if (floor((origin.x + x) * a + b) > origin.y + y && origin.x != destination.x) {
    //         origin.x += x;
    //     } else {
    //         origin.y += y;
    //     }
    // }

    // return {origin.x, origin.y};
}
