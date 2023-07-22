#include <raylib.h>
#include "ball.h"

int main()
{
    Color darkGreen = Color{20, 160, 133, 255};

    int display = GetCurrentMonitor();

    Ball ball = Ball();

    InitWindow(GetMonitorWidth(display), GetMonitorHeight(display), "main");
    ToggleFullscreen();
    HideCursor();
    SetTargetFPS(60);

    while (!WindowShouldClose())
    {
        BeginDrawing();
        ClearBackground(darkGreen);
        ball.Update();
        ball.Draw();
        EndDrawing();
    }

    CloseWindow();
    return 0;
}