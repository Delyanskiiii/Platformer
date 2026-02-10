#include "dynamic.h"
#include <cmath>
#include <iostream>

Dynamic::Dynamic(Texture2D texture, int layer, Vector2 position, Image collisionImage) : Static(texture, layer, position) {
    this->collisionImage = collisionImage;
}

void Dynamic::SetPosition(Vector2 position) {
    this->position = position;
}

void Dynamic::SetAccuratePosition(Vector2 position) {
    this->accuratePosition = {position.x, position.y};
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

void Dynamic::Translate(Vector2 accurateDestination) {
    ImVec2 destination = {int(floor(accurateDestination.x / 10 + 0.5)), int(floor(accurateDestination.y / 10 + 0.5))};
    Vector2 variance = {float(Direction(destination.x, this->position.x)), float(Direction(destination.y, this->position.y))};
    float a = float(destination.y - this->position.y) / (destination.x - this->position.x);
    float b = destination.y - a * destination.x;
    Vector2 currentPixelLocation = this->position;
    bool horizontal;
    int horizontalOverflow = 0;
    int verticalOverflow = 0;

    while (currentPixelLocation.x != destination.x || currentPixelLocation.y != destination.y) {
        if (abs(destination.x - this->position.x) < abs(destination.y - this->position.y)) {
            if (currentPixelLocation.y == destination.y || floor((currentPixelLocation.x + variance.x) * a + b + 0.5) == currentPixelLocation.y) {
                currentPixelLocation.x += variance.x;
                horizontal = true;
            } else {
                currentPixelLocation.y += variance.y;
                horizontal = false;
            }
        } else {
            if (currentPixelLocation.x == destination.x || floor((currentPixelLocation.y + variance.y - b) / a + 0.5) == currentPixelLocation.x) {
                currentPixelLocation.y += variance.y;
                horizontal = false;
            } else {
                currentPixelLocation.x += variance.x;
                horizontal = true;
            }
        }

        if ((int)GetImageColor(this->collisionImage, currentPixelLocation.x, currentPixelLocation.y).a != 0) {
            if (horizontal) {
                currentPixelLocation.x -= variance.x;
                if (currentPixelLocation.y == destination.y) {
                    horizontalOverflow = abs(currentPixelLocation.x - destination.x);
                    break;
                } else {
                    currentPixelLocation.y += variance.y;
                    if ((int)GetImageColor(this->collisionImage, currentPixelLocation.x, currentPixelLocation.y).a != 0) {
                        currentPixelLocation.y -= variance.y;
                        horizontalOverflow = abs(currentPixelLocation.x - destination.x);
                        verticalOverflow = abs(currentPixelLocation.y - destination.y);
                        break;
                    }
                }
            } else {
                currentPixelLocation.y -= variance.y;
                if (currentPixelLocation.x == destination.x) {
                    verticalOverflow = abs(currentPixelLocation.y - destination.y);
                    break;
                } else {
                    currentPixelLocation.x += variance.x;
                    if ((int)GetImageColor(this->collisionImage, currentPixelLocation.x, currentPixelLocation.y).a != 0) {
                        currentPixelLocation.x -= variance.x;
                        horizontalOverflow = abs(currentPixelLocation.x - destination.x);
                        verticalOverflow = abs(currentPixelLocation.y - destination.y);
                        break;
                    }
                }
            }
        }
    }

    SetPosition(currentPixelLocation);
    if (horizontalOverflow == 0 && verticalOverflow == 0) {
        SetAccuratePosition(accurateDestination);
    } else if (horizontalOverflow > 0 && verticalOverflow == 0) {
        SetAccuratePosition({currentPixelLocation.x * 10, accurateDestination.y});
    } else if (horizontalOverflow == 0 && verticalOverflow > 0) {
        SetAccuratePosition({accurateDestination.x, currentPixelLocation.y * 10});
    } else {
        SetAccuratePosition({currentPixelLocation.x * 10, currentPixelLocation.y * 10});
    }

    return;
}

void Dynamic::Update() {

}

int Dynamic::VerticallyGrounded(Vector2 location) {
    if ((int)GetImageColor(this->collisionImage, location.x, location.y + 1).a != 0 && (int)GetImageColor(this->collisionImage, location.x, location.y - 1).a != 0) {
        return 2;
    } if ((int)GetImageColor(this->collisionImage, location.x, location.y + 1).a != 0) {
        return 1;
    } else if ((int)GetImageColor(this->collisionImage, location.x, location.y - 1).a != 0) {
        return -1;    
    } else {
        return 0;
    }
}

int Dynamic::HorizontallyGrounded(Vector2 location) {
    if ((int)GetImageColor(this->collisionImage, location.x + 1, location.y).a != 0 && (int)GetImageColor(this->collisionImage, location.x - 1, location.y).a != 0) {
        return 2;
    } if ((int)GetImageColor(this->collisionImage, location.x + 1, location.y).a != 0) {
        return 1;
    } else if ((int)GetImageColor(this->collisionImage, location.x - 1, location.y).a != 0) {
        return -1;    
    } else {
        return 0;
    }
}