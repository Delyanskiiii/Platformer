#include "player.h"
#include <cstring>
#include <cmath>
#include <iostream>
 
void Player::Update() {
    input = inputer.GetInput();
    if (Grounded(this->position)) {
        grounded = true;
        velocity.y = 0;
    } else {
        grounded = false;
    }

    if (strcmp(state, "idle") == 0) {
        if (!grounded || input.y < 0) {
            this->state = "glide";
        } else if (input.x != 0 && input.y > 0) {
            this->state = "slide";
        } else if (input.x != 0) {
            this->state = "run";
        }
    } else if (strcmp(state, "run") == 0) {
        if (!grounded || input.y < 0) {
            this->state = "glide";
        } else if (input.x == 0 && input.y == 0 && velocity.x == 0) {
            this->state = "idle";
        } else if ((input.x != 0 || velocity.x != 0) && input.y > 0) {
            this->state = "slide";
        }
    } else if (strcmp(state, "glide") == 0) {
        if (input.x == 0 && input.y == 0 && velocity.x == 0 && velocity.y == 0 && grounded) {
            this->state = "idle";
        } else if ((input.x != 0 || velocity.x != 0) && input.y == 0 && grounded) {
            this->state = "run";
        } else if ((input.x != 0 || velocity.x != 0) && input.y > 0 && grounded) {
            this->state = "slide";
        }
    } else if (strcmp(state, "slide") == 0) {
        if (!grounded || input.y < 0) {
            this->state = "glide";
        } else if (input.x == 0 && input.y == 0 && velocity.x == 0 && velocity.y == 0) {
            this->state = "idle";
        } else if ((input.x != 0 || velocity.x != 0) && input.y == 0) {
            this->state = "run";
        }
    }

    if (strcmp(state, "idle") == 0) {
        
    } else if (strcmp(state, "run") == 0) {
        
        if (input.x != 0) {
            velocity.x += input.x * runningAcceleration;
        } else {
            if (velocity.x > 0) {
                velocity.x -= runningAcceleration;
            } else if (velocity.x < 0) {
                velocity.x += runningAcceleration;
            }
        }

        if (velocity.x > maxRunningSpeed) {
            velocity.x = maxRunningSpeed;
        } else if (velocity.x < -maxRunningSpeed) {
            velocity.x = -maxRunningSpeed;
        }
        velocity.y = 0;
    } else if (strcmp(state, "glide") == 0) {
        velocity.x = input.x * runningAcceleration * 2;
        if (input.y < 0 && grounded) {
            velocity.y = input.y * jumpSpeed;
        } else if (input.y < 0) {
            velocity.y += 5;
        } else {
            velocity.y += 2;
        }
    } else if (strcmp(state, "slide") == 0) {
        // velocity.x -= input.x * slidingDeceleration;
        velocity.x = 0;
        velocity.y = 0;
    }
    // RenderTexture texture;

    this->accuratePosition.x += velocity.x;
    this->accuratePosition.y += velocity.y;

    Translate({(int)(this->accuratePosition.x / 10), (int)(this->accuratePosition.y / 10)});

}