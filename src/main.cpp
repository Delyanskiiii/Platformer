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

    Color skyBlue = Color{135, 206, 235, 255};
    Texture2D texFull = LoadTexture("texture.png");
    Shader shader = LoadShader(0, TextFormat("src/lights.fs"));

    // SetShaderValue(shader, GetShaderLocation(shader, "Texture"), &texFull, SHADER_UNIFORM_SAMPLER2D);
    // SetShaderValueTexture(shader, GetShaderLocation(shader, "Texture"), texFull);

    Vector2 light_props = {5, 100};
    SetShaderValue(shader, GetShaderLocation(shader, "lightProps"), &light_props, SHADER_UNIFORM_VEC2);

    RenderTexture2D target = LoadRenderTexture(targetWidth, targetHeight);

    ToggleFullscreen();
    HideCursor();
    SetTargetFPS(50);

    World world = World(targetWidth, targetHeight);
    int lightSource_index = GetShaderLocation(shader, "lightSource");

    while (!WindowShouldClose())
    {
        Vector2 player_position = world.Update();
        SetShaderValue(shader, lightSource_index, &player_position, SHADER_UNIFORM_VEC2);
        BeginTextureMode(target);
            ClearBackground(skyBlue);

            BeginShaderMode(shader);
                world.Draw();
            EndShaderMode();
        EndTextureMode();

        BeginDrawing();
            ClearBackground(skyBlue);
            DrawTexturePro(target.texture, (Rectangle){0, 0, targetWidth, -targetHeight}, (Rectangle){0, 0, displayWidth, displayHeight}, (Vector2){0, 0}, 0.0f, WHITE);
            DrawFPS(displayWidth - 80, 0);
        EndDrawing();
    }
    UnloadShader(shader);
    UnloadTexture(texFull);
    UnloadRenderTexture(target);
    CloseWindow();
    return 0;
}