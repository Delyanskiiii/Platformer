#include <raylib.h>
#include "player.h"

int main()
{
    Color skyBlue = Color{135, 206, 235, 255};

    int display = GetCurrentMonitor();

    Player player = Player();

    InitWindow(GetMonitorWidth(display), GetMonitorHeight(display), "main");
    ToggleFullscreen();
    HideCursor();
    SetTargetFPS(100);

    while (!WindowShouldClose())
    {
        BeginDrawing();
        ClearBackground(skyBlue);
        player.Update();
        player.Draw();
        EndDrawing();
    }

    CloseWindow();
    return 0;
}