#include "renderer.h"
#include <iostream>

Renderer::Renderer() {

}

Vector2 Renderer::Update() {
    input = inputer.GetInput();
    this->player.Update(input);
    return this->player.GetPosition();
}

void Renderer::Draw() {
    this->backGround.Draw();
    this->middleGround.Draw();
    this->player.Draw();
}

void Renderer::Debug() {
    ImGui::Text("Input: %d, %d", int(input.x), int(input.y));
    // int health = 100;
    // float score = 95.5f;
    // const char* name = "Player1";

    // ImGui::Text("Health: %d", health);
    // ImGui::Text("Score: %.1f", score);
    // ImGui::Text("Name: %s", name);

    player.Debug();
}