#include <raylib.h>
#include <iostream>
#include "world.h"
#include "constants.h"

int main()
{
    InitWindow(0, 0, "main");
    int display = GetCurrentMonitor();
    int displayWidth = GetMonitorWidth(display);
    int displayHeight = GetMonitorHeight(display);

    Color skyBlue = Color{135, 206, 235, 255};
    Shader shader = LoadShader(0, TextFormat("shaders/shadow.fs"));

    Vector2 light_props = {5, 100};
    SetShaderValue(shader, GetShaderLocation(shader, "lightProps"), &light_props, SHADER_UNIFORM_VEC2);

    RenderTexture2D target = LoadRenderTexture(TARGET_WIDTH, TARGET_HEIGHT);

    ToggleFullscreen();
    HideCursor();
    SetTargetFPS(50);

    World world = World();
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
            DrawTexturePro(target.texture, (Rectangle){0, 0, float(TARGET_WIDTH), float(-TARGET_HEIGHT)}, (Rectangle){0, 0, float(displayWidth), float(displayHeight)}, (Vector2){0, 0}, 0.0f, WHITE);
            DrawFPS(displayWidth - 80, 0);
        EndDrawing();
    }
    UnloadShader(shader);
    UnloadRenderTexture(target);
    CloseWindow();
    return 0;
}