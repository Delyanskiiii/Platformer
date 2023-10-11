#include <raylib.h>
#include <iostream>
#include "world.h"

int main()
{
    Color skyBlue = Color{135, 206, 235, 255};

    int display = GetCurrentMonitor();

    InitWindow(GetMonitorWidth(display), GetMonitorHeight(display), "main");

    ToggleFullscreen();
    HideCursor();
    SetTargetFPS(100);

    int diff = GetMonitorWidth(display) / 320;

    World world = World(diff);

    while (!WindowShouldClose())
    {
        BeginDrawing();
        ClearBackground(skyBlue);
        DrawFPS(2480, 0);
        world.Update();
        world.Draw();
        EndDrawing();
    }

    CloseWindow();
    return 0;
}