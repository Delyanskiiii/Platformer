#include <raylib.h>
#include <iostream>
#include "renderer.h"
#include "constants.h"
int main()
{
    SetConfigFlags(FLAG_MSAA_4X_HINT);
    InitWindow(0, 0, "Main");
    int display = GetCurrentMonitor();
    int displayWidth = GetMonitorWidth(display);
    int displayHeight = GetMonitorHeight(display);

    Color skyBlue = Color{135, 206, 235, 255};
    Shader shader = LoadShader(0, TextFormat("src/engine/shaders/shadow.fs"));

    Vector2 light_range = {400, 0};
    SetShaderValue(shader, GetShaderLocation(shader, "lightRange"), &light_range, SHADER_UNIFORM_VEC2);

    RenderTexture2D target = LoadRenderTexture(TARGET_WIDTH, TARGET_HEIGHT);

    ToggleFullscreen();
    HideCursor();
    SetTargetFPS(100);

    Renderer renderer = Renderer();
    int lightSource_index = GetShaderLocation(shader, "lightSource");

    rlImGuiSetup(true);

    while (!WindowShouldClose())
    {
        Vector2 playerPosition = renderer.Update();
        SetShaderValue(shader, lightSource_index, &playerPosition, SHADER_UNIFORM_VEC2);
        BeginTextureMode(target);
            ClearBackground(skyBlue);
            
            BeginShaderMode(shader);
                renderer.Draw();
            EndShaderMode();

        EndTextureMode();

        BeginDrawing();
            ClearBackground(skyBlue);
            rlImGuiBegin();
            bool open = true;

            if (ImGui::Begin("Test Window", &open))
            {
                renderer.Debug();
            }
            ImGui::End();
            DrawTexturePro(target.texture, (Rectangle){0, 0, float(TARGET_WIDTH), float(-TARGET_HEIGHT)}, (Rectangle){0, 0, float(displayWidth), float(displayHeight)}, (Vector2){0, 0}, 0.0f, WHITE);
            DrawFPS(displayWidth - 80, 0);
            rlImGuiEnd();
        EndDrawing();
    }
    rlImGuiShutdown();
    UnloadShader(shader);
    UnloadRenderTexture(target);
    CloseWindow();
    return 0;
}