#include <raylib.h>
#include <iostream>
#include "player.h"
#include "input.h"

int diff;

int main()
{
    Color skyBlue = Color{135, 206, 235, 255};

    int display = GetCurrentMonitor();

    Player player = Player();
    Input input = Input();

    InitWindow(GetMonitorWidth(display), GetMonitorHeight(display), "main");

    ToggleFullscreen();
    HideCursor();
    SetTargetFPS(100);

    diff = GetMonitorWidth(display) / 320;

    while (!WindowShouldClose())
    {
        BeginDrawing();
        ClearBackground(skyBlue);
        player.Update(input.GetInput(IsKeyDown(KEY_LEFT), IsKeyDown(KEY_RIGHT), IsKeyDown(KEY_SPACE), IsKeyDown(KEY_DOWN)));
        player.Draw();
        EndDrawing();
    }

    CloseWindow();
    return 0;
}