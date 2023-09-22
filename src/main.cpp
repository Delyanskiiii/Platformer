#include <raylib.h>
#include <iostream>
#include "player.h"
#include "input.h"
#include "world.h"
#include "main.h"

int main()
{
    Color skyBlue = Color{135, 206, 235, 255};

    int display = GetCurrentMonitor();

    Player player = Player();
    Input input = Input();
    World world = World();

    InitWindow(GetMonitorWidth(display), GetMonitorHeight(display), "main");

    ToggleFullscreen();
    HideCursor();
    SetTargetFPS(100);

    diff = GetMonitorWidth(display) / 320;
    // world.Draw();
    while (!WindowShouldClose())
    {
        BeginDrawing();
        ClearBackground(skyBlue);
        DrawFPS(2480, 0);
        world.Draw();
        player.Update(input.GetInput(IsKeyDown(KEY_LEFT), IsKeyDown(KEY_RIGHT), IsKeyDown(KEY_SPACE), IsKeyDown(KEY_DOWN)));
        player.Draw();
        EndDrawing();
    }

    CloseWindow();
    return 0;
}