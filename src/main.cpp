#include <raylib.h>
#include <iostream>
#include "world.h"

int main()
{
    int display = GetCurrentMonitor();
    InitWindow(GetMonitorWidth(display), GetMonitorHeight(display), "main");

    Color skyBlue = Color{135, 206, 235, 255};
    Texture2D texFull = LoadTexture("levels/light_test.png");
    Shader shader = LoadShader(0, TextFormat("src/shader.fs"));
    SetShaderValue(shader, GetShaderLocation(shader, "ourTexture"), &texFull, SHADER_UNIFORM_VEC2);

    Vector2 light_strength_value = {5, 5};
    SetShaderValue(shader, GetShaderLocation(shader, "lightStrength"), &light_strength_value, SHADER_UNIFORM_VEC2);

    ToggleFullscreen();
    HideCursor();
    SetTargetFPS(100);

    int diff = GetMonitorWidth(display) / 320;

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






// #include "raylib.h"

// #if defined(PLATFORM_DESKTOP)
//     #define GLSL_VERSION            330
// #else   // PLATFORM_ANDROID, PLATFORM_WEB
//     #define GLSL_VERSION            100
// #endif

// //------------------------------------------------------------------------------------
// // Program main entry point
// //------------------------------------------------------------------------------------
// int main(void)
// {
//     // Initialization
//     //--------------------------------------------------------------------------------------
//     const int screenWidth = 800;
//     const int screenHeight = 450;

//     InitWindow(screenWidth, screenHeight, "raylib - multiple sample2D");

//     // Image imRed = GenImageColor(800, 450, (Color){ 255, 0, 0, 255 });
//     Texture texRed = LoadTexture("levels/test.png");
//     Texture texFull = LoadTexture("levels/full.png");
//     // UnloadImage(imRed);

//     // Image imBlue = GenImageColor(800, 450, (Color){ 0, 0, 255, 255 });
//     // Texture texBlue = LoadTextureFromImage(imBlue);
//     // UnloadImage(imBlue);
//     // Color transparentColor = (Color){ 0, 0, 0, 255 };

//     Shader shader = LoadShader(0, TextFormat("src/shader.fs", GLSL_VERSION));

//     // // Get an additional sampler2D location to be enabled on drawing
//     // int texBlueLoc = GetShaderLocation(shader, "texture1");

//     // Get shader uniform for divider
//     // int light_source_location = GetShaderLocation(shader, "light_source");
//     // Vector2 light_source_value = {0, 0};

//     SetTargetFPS(60);                           // Set our game to run at 60 frames-per-second
//     //--------------------------------------------------------------------------------------

//     // Main game loop
//     while (!WindowShouldClose())                // Detect window close button or ESC key
//     {
//         // Update
//         //----------------------------------------------------------------------------------
//         // if (IsKeyDown(KEY_RIGHT)) dividerValue += 0.01f;
//         // else if (IsKeyDown(KEY_LEFT)) dividerValue -= 0.01f;

//         // if (dividerValue < 0.0f) dividerValue = 0.0f;
//         // else if (dividerValue > 1.0f) dividerValue = 1.0f;

//         // SetShaderValue(shader, light_source_location, &light_source_value, SHADER_UNIFORM_FLOAT);
//         //----------------------------------------------------------------------------------

//         // Draw
//         //----------------------------------------------------------------------------------
//         BeginDrawing();

//             ClearBackground(RAYWHITE);

//             BeginShaderMode(shader);

//                 // WARNING: Additional samplers are enabled for all draw calls in the batch,
//                 // EndShaderMode() forces batch drawing and consequently resets active textures
//                 // to let other sampler2D to be activated on consequent drawings (if required)
                
//                 // SetShaderValueTexture(shader, texBlueLoc, texBlue);

//                 // We are drawing texRed using default sampler2D texture0 but
//                 // an additional texture units is enabled for texBlue (sampler2D texture1)
//                 // DrawTexture(texRed, 10, 0, WHITE);
//                 DrawTextureEx(texRed, { 40, 0 }, 0, 8, WHITE);
//                 DrawTextureEx(texRed, { 40, 40 }, 0, 8, WHITE);
//                 DrawTextureEx(texRed, { 80, 0 }, 0, 8, WHITE);
//                 DrawTextureEx(texFull, { 80, 40 }, 0, 8, WHITE);
//                 DrawTextureEx(texRed, { 240, 160 }, 0, 8, WHITE);

//             EndShaderMode();

//             DrawText("Use KEY_LEFT/KEY_RIGHT to move texture mixing in shader!", 80, GetScreenHeight() - 40, 20, RAYWHITE);

//         EndDrawing();
//         //----------------------------------------------------------------------------------
//     }

//     // De-Initialization
//     //--------------------------------------------------------------------------------------
//     UnloadShader(shader);       // Unload shader
//     UnloadTexture(texRed);      // Unload texture
//     UnloadTexture(texFull);     // Unload texture

//     CloseWindow();              // Close window and OpenGL context
//     //--------------------------------------------------------------------------------------

//     return 0;
// }






// #include "raylib.h"
// // #include "rlgl.h"

// int main()
// {
//     int display = GetCurrentMonitor();
//     float screenWidth = GetMonitorWidth(display);
//     float screenHeight = GetMonitorHeight(display);

//     InitWindow(screenWidth, screenHeight, "main");
//     ToggleFullscreen();
//     HideCursor();

//     // Shader shader = LoadShader(TextFormat("src/sample_vertex.vert"), TextFormat("src/sample_fragment.frag"));

//     Shader shader = LoadShader(0, TextFormat("src/shader.fs"));

//     Texture texFull = LoadTexture("levels/full.png");

//     // Vector2 light_source_value = {screenWidth / 2, screenHeight / 2};
//     // Vector2 resolution_value = {screenWidth, screenHeight};
//     // Vector2 light_strength_value = {30, 100};
//     Vector2 position = {0, 0};
//     Vector2 positionMid = {500, 500};
//     SetShaderValue(shader, GetShaderLocation(shader, "ourTexture"), &texFull, SHADER_UNIFORM_VEC2);

//     // SetShaderValue(shader, GetShaderLocation(shader, "lightPosition"), &light_source_value, SHADER_UNIFORM_VEC2);
//     // SetShaderValue(shader, GetShaderLocation(shader, "resolution"), &resolution_value, SHADER_UNIFORM_VEC2);
//     // SetShaderValue(shader, GetShaderLocation(shader, "lightStrength"), &light_strength_value, SHADER_UNIFORM_VEC2);

//     // Main game loop
//     while (!WindowShouldClose())
//     {
//         // Update

//         // Draw
//         BeginDrawing();

//         ClearBackground(RAYWHITE);
//         DrawFPS(2480, 0);
//         // Set the shader before drawing
//         DrawTextureEx(texFull, positionMid, 0, 100, WHITE);
//         // DrawTextureEx(texFull, position, 0, 100, WHITE);
//         BeginShaderMode(shader);

//         DrawTextureEx(texFull, position, 0, 100, WHITE);
//         // DrawTextureEx(texFull, positionMid, 0, 100, WHITE);
//         // DrawTexturePro(texFull, (Rectangle){ 0, 0, screenWidth, -screenHeight },(Rectangle){ 0, 0, (float)screenWidth, (float)screenHeight }, (Vector2){ 0, 0 }, 0.0f, WHITE);

//         // Reset the shader after drawing
//         EndShaderMode();

//         EndDrawing();
//     }

//     // Unload resources
//     UnloadShader(shader);

//     CloseWindow();

//     return 0;
// }