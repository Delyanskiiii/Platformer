#include <raylib.h>
#include <iostream>
#include "player.h"
// #include "world.h"

int main()
{
    Color skyBlue = Color{135, 206, 235, 255};

    int display = GetCurrentMonitor();

    InitWindow(GetMonitorWidth(display), GetMonitorHeight(display), "main");

    ToggleFullscreen();
    HideCursor();
    SetTargetFPS(100);

    int diff = GetMonitorWidth(display) / 320;

    Player player = Player(diff);
    // World world = World();

    while (!WindowShouldClose())
    {
        BeginDrawing();
        ClearBackground(skyBlue);
        DrawFPS(2480, 0);
        player.Update();
        // world.Update(player.position());
        // world.Draw();
        player.Draw();
        EndDrawing();
    }

    CloseWindow();
    return 0;
}