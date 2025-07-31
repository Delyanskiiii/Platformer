#include "renderer.h"
#include <iostream>

Renderer::Renderer() {

}

Vector2 Renderer::Update() {
    this->player.Update();
    return this->player.GetPosition();
}

void Renderer::Draw() {
    this->backGround.Draw();
    this->middleGround.Draw();
    this->player.Draw();
}