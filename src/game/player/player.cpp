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

    switch (currentState)
    {
        case IDLE:
        {
            if (!grounded || input.y < 0) {
                currentState = GLIDE;
            } else if (input.x != 0 && input.y > 0) {
                currentState = SLIDE;
            } else if (input.x != 0) {
                currentState = RUN;
            }
        } break;
        case RUN:
        {
            if (!grounded || input.y < 0) {
                currentState = GLIDE;
            } else if (input.x == 0 && input.y == 0 && velocity.x == 0) {
                currentState = IDLE;
            } else if ((input.x != 0 || velocity.x != 0) && input.y > 0) {
                currentState = SLIDE;
            }
        } break;
        case SLIDE:
        {
            if (!grounded || input.y < 0) {
                currentState = GLIDE;
            } else if (input.x == 0 && input.y == 0 && velocity.x == 0 && velocity.y == 0) {
                currentState = IDLE;
            } else if ((input.x != 0 || velocity.x != 0) && input.y == 0) {
                currentState = RUN;
            }
        } break;
        case GLIDE:
        {
            if (input.x == 0 && input.y == 0 && velocity.x == 0 && velocity.y == 0 && grounded) {
                currentState = IDLE;
            } else if ((input.x != 0 || velocity.x != 0) && input.y == 0 && grounded) {
                currentState = RUN;
            } else if ((input.x != 0 || velocity.x != 0) && input.y > 0 && grounded) {
                currentState = SLIDE;
            }
        } break;
        default: break;
    }

    // position.x += input.x;
    // position.y += input.y;

    switch (currentState)
    {
        case IDLE:
        {

        } break;
        case RUN:
        {
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
        } break;
        case SLIDE:
        {
            // velocity.x -= input.x * slidingDeceleration;
            velocity.x = 0;
            velocity.y = 0;
        } break;
        case GLIDE:
        {
            velocity.x = input.x * runningAcceleration * 2;
            if (input.y < 0 && grounded) {
                velocity.y = input.y * jumpSpeed;
            } else if (input.y < 0) {
                velocity.y += 5;
            } else {
                velocity.y += 2;
            }
        } break;
        default: break;
    }

    this->accuratePosition.x += velocity.x;
    this->accuratePosition.y += velocity.y;

    Translate(accuratePosition);

}

void Player::Debug() {
    switch(currentState) {
        case State::IDLE: ImGui::Text("State: IDLE"); break;
        case State::GLIDE: ImGui::Text("State: GLIDE"); break;
        case State::SLIDE: ImGui::Text("State: SLIDE"); break;
        case State::RUN: ImGui::Text("State: RUN"); break;
        default: ImGui::Text("State: IDLE");
    }
    ImGui::Text("Input: %d, %d", int(input.x), int(input.y));
    ImGui::Text("accuratePosition: %d, %d", int(accuratePosition.x), int(accuratePosition.y));
    ImGui::Text("Position: %d, %d", int(position.x), int(position.y));
    ImGui::Text("Velocity: %d, %d", int(velocity.x), int(velocity.y));
}