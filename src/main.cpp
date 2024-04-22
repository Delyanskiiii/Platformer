#include <raylib.h>
#include <iostream>
#include "world.h"

int main()
{
    int display = GetCurrentMonitor();
    int width = GetMonitorWidth(display);
    int height = GetMonitorHeight(display);
    InitWindow(width, height, "main");

    Color skyBlue = Color{135, 206, 235, 255};
    Texture2D texFull = LoadTexture("levels/light_test.png");
    Shader shader = LoadShader(0, TextFormat("src/shader.fs"));
    SetShaderValue(shader, GetShaderLocation(shader, "ourTexture"), &texFull, SHADER_UNIFORM_VEC2);

    int diff = GetMonitorWidth(display) / 320;
    SetShaderValue(shader, GetShaderLocation(shader, "resolution"), &diff, SHADER_UNIFORM_VEC2);

    Vector2 light_strength_value = {5 * diff, 5 * diff};
    SetShaderValue(shader, GetShaderLocation(shader, "lightStrength"), &light_strength_value, SHADER_UNIFORM_VEC2);

    ToggleFullscreen();
    HideCursor();
    SetTargetFPS(100);

    World world = World(diff);
    int loc_index = GetShaderLocation(shader, "lightPosition");

    while (!WindowShouldClose())
    {
        Vector2 player_position = world.Update();
        SetShaderValue(shader, loc_index, &player_position, SHADER_UNIFORM_VEC2);

        BeginDrawing();
        ClearBackground(skyBlue);
        DrawFPS(2480, 0);
        BeginShaderMode(shader);
        world.Draw();
        EndShaderMode();
        EndDrawing();
    }
    UnloadShader(shader);
    UnloadTexture(texFull);
    CloseWindow();
    return 0;
}