#include <raylib.h>
#include <iostream>
#include "world.h"

int main()
{
    InitWindow(0, 0, "main");
    int display = GetCurrentMonitor();
    int displayWidth = GetMonitorWidth(display);
    int displayHeight = GetMonitorHeight(display);

    int targetWidth = 320;
    int targetHeight = 180;

    Vector2 resolution = {targetWidth, targetHeight};

    Color skyBlue = Color{135, 206, 235, 255};
    Texture2D texFull = LoadTexture("levels/light_test.png");
    Shader shader = LoadShader(0, TextFormat("src/shader.fs"));
    SetShaderValue(shader, GetShaderLocation(shader, "ourTexture"), &texFull, SHADER_UNIFORM_VEC2);

    SetShaderValue(shader, GetShaderLocation(shader, "resolution"), &resolution, SHADER_UNIFORM_VEC2);

    Vector2 light_strength_value = {100, 5};
    SetShaderValue(shader, GetShaderLocation(shader, "lightStrength"), &light_strength_value, SHADER_UNIFORM_VEC2);

    RenderTexture2D target = LoadRenderTexture(targetWidth, targetHeight);

    ToggleFullscreen();
    HideCursor();
    SetTargetFPS(100);

    World world = World(targetWidth, targetHeight);
    int loc_index = GetShaderLocation(shader, "lightPosition");

    while (!WindowShouldClose())
    {
        Vector2 player_position = world.Update();
        SetShaderValue(shader, loc_index, &player_position, SHADER_UNIFORM_VEC2);

        BeginTextureMode(target);
            ClearBackground(skyBlue);

            BeginShaderMode(shader);
                world.Draw();
            EndShaderMode();
        EndTextureMode();

        BeginDrawing();
            ClearBackground(skyBlue);
            DrawFPS(2480, 0);
        
            DrawTexturePro(target.texture, (Rectangle){0, 0, 320, -180}, (Rectangle){0, 0, displayWidth, displayHeight}, (Vector2){0, 0}, 0.0f, WHITE);
        EndDrawing();
    }
    UnloadShader(shader);
    UnloadTexture(texFull);
    UnloadRenderTexture(target);
    CloseWindow();
    return 0;
}